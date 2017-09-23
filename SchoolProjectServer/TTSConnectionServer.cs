using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

// This class manages the connection between TTS Client and Server
// The implementation is to be used on both sides to maintain integrity

namespace SchoolProjectServer
{
    public sealed class StateObject
    {
        /* Contains the state information. */
        public Socket mSocket = null;
        public const int Buffer_Size = 1024;
        public byte[] mBuffer = new byte[Buffer_Size];
        public StringBuilder mSb;

        public StateObject()
        {
            this.Reset();

        }

        public bool Close { get; set; }

        public string Text
        {
            get
            {
                return this.mSb.ToString();
            }
        }

        public void Append(string text)
        {
            this.mSb.Append(text);
        }

        public void Reset()
        {
            this.mSb = new StringBuilder();
        }

        public bool isDone()
        {
            string result = mSb.ToString();
            return (result.IndexOf("<EOF>") > -1);
        }
    }

    public class TTSConnectionServer
    {
        // Enumerated constants
        private enum Envelope : byte
        {
            protocolVersion = 0,    // Only client and server with same protocol level can talk to each other (int) (C,S)
            tweetTweets,            // Requests tweet list (no param) (C)
                                    // Shows the count of the tweets to be expected from the server (int) (S)
                                    // Container for tweetText,tweetDateTime,tweetUpvotes,tweetDownvotes. Contains tweet ID (long) (S)
            tweetText,              // Contains text for one tweet (string, length<=256) (S)
            tweetDateTime,          // Contains the DateTime for one tweet (DateTime) (S)
            tweetUpvotes,           // Contains the upvote count for one tweet (int) (S)
            tweetDownvotes,         // Contains the downvote count for one tweet (int) (S)
            tweetStyleList,         // Marks the beginning of a list of tweetStyleElements, contains a number of styles to be expected (int) (S)
            tweetStyleElement,      // Contains the name of one tweet style (string) (S)
            tweetStylePictureURL,   // Contains URL for the tweet picture for one style (string) (S)
            tweetUpvote,            // Contains one tweet number for which the server increases the upvote count (C)
            tweetDownvote,          // Contains one tweet number for which the server increases the downvote count (C)
            tweetProtocolError,     // Contains the envelope code which led to failure on the other end
            tweetEOT                // End of transmission (C,S)
        }

        private enum ServerState
        {
            stateStart = 0,         // The default state before processing the first line of the server's response
            stateProtocolVersion,   // Next line is the protocol version
            stateStyleList,         // Next line contains how many styles are to be expected
            stateStyleElement,      // The two next lines are : style name, style picture url
            stateTweets,            // Next line contains the number of tweets to be expected, then the following structure follows:
                                    // (tweetText,tweetDateTime,tweetUpvotes,tweetDownvotes) as many times as the Count predicts
            stateEOT,               // The server is closing the connection
            stateProtocolError     // Erroneous answer received, can't process it.
        }

        //        private TcpListener listener = new TcpListener(new IPAddress(0x2130706433f), 7756);


        // Constants
        private const int COMMUNICATION_PORT = 7756;
        private const long PROTOCOL_VERSION = 0xA000;
        private const int SERVER_LIMIT = 8;
        private const string EOF_STRING = "<EOF>";
        private const int TWEETS_TO_SEND = 8;

        // Variables
        IPEndPoint mIPEndPoint;
        string mLastStatus;
        MainForm mOwner;
        List<TweetStyle> mTweetStyles;
        List<Tweet> mTweets;
        // Events

        // ManualResetEvent instances signal completion.  
        private static ManualResetEvent EConnectDone = new ManualResetEvent(false);
        private static ManualResetEvent ESendDone = new ManualResetEvent(false);
        private static ManualResetEvent EReceiveDone = new ManualResetEvent(false);

        public static ManualResetEvent EAllDone = new ManualResetEvent(false);

        // Helper functions

        private static void ShowErrorDialog(string pMessage)
        {
            System.Console.WriteLine(pMessage);
            //MessageBox.Show(pMessage, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public string GetLastStatusMessage()
        {
            return mLastStatus;
        }

        // Constructors
        public TTSConnectionServer(MainForm pOwner)
        {
            mTweetStyles = new List<TweetStyle>();
            mTweets = new List<Tweet>();
            mOwner = pOwner;
        }

        public void StartListening()
        {
            mLastStatus = "Building local endpoint";
            try
            {
                //IPHostEntry lIPHostInfo = Dns.GetHostEntry("localhost");//Dns.GetHostName());
                IPAddress lIPAddress = new IPAddress(2130706433);//lIPHostInfo.AddressList[0];
                bool lIpFound = false;

                //IPHostEntry host = Dns.GetHostEntry("89.132.188.93");
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        lIPAddress = ip;
                        lIpFound = true;
                        break;
                    }
                }
                if (!lIpFound)
                {
                    mLastStatus = "No suitable local IP address found.";
                    return;
                }
                Socket lListenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                mIPEndPoint = new IPEndPoint(lIPAddress, COMMUNICATION_PORT);
                mLastStatus = "Working as a server. Listening on port " + mIPEndPoint.Port.ToString();
                try
                {
                    lListenerSocket.Bind(mIPEndPoint);
                    lListenerSocket.Listen(1);

                    //EConnectDone.WaitOne(); ??
                    while (true)
                    {
                        // Set the event to nonsignaled state.  
                        EAllDone.Reset();

                        // Start an asynchronous socket to listen for connections.  
                        mLastStatus = "Waiting for a connection...";
                        lListenerSocket.BeginAccept(
                            new AsyncCallback(AcceptCallback),
                            lListenerSocket);

                        // Wait until a connection is made before continuing.  
                        //Application.DoEvents(); Memory-thief
                        EAllDone.WaitOne(new TimeSpan(0, 0, 1));
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            catch (SocketException ex)
            {
                ShowErrorDialog(ex.Message);
            }
            catch (ObjectDisposedException ex)
            {
                ShowErrorDialog(ex.Message);
            }
            return;
        }

        public void AcceptCallback(IAsyncResult AR)
        {
            Console.WriteLine("AcceptCallBack");
            // Signal the main thread to continue.  
            EAllDone.Set();

            // Get the socket that handles the client request.  
            Socket lListenerSocket = (Socket)AR.AsyncState;
            Socket lHandler = lListenerSocket.EndAccept(AR);

            // Create the state object.  
            StateObject lStateObject = new StateObject();
            lStateObject.mSocket = lHandler;
            lHandler.BeginReceive(lStateObject.mBuffer, 0, StateObject.Buffer_Size, 0,
                new AsyncCallback(ReadCallback), lStateObject);
        }

        public void ReadCallback(IAsyncResult AR)
        {
            try
            {
                StateObject lStateObject = (StateObject)AR.AsyncState;
                Socket lHandler = lStateObject.mSocket;
                string lData;

                int lReceivedBytes = lHandler.EndReceive(AR);

                if (lReceivedBytes > 0)
                {
                    lStateObject.mSb.Append(Encoding.ASCII.GetString(lStateObject.mBuffer, 0, lReceivedBytes));
                    if (lStateObject.isDone())
                    {
                        lData = lStateObject.mSb.ToString();
                        ServerHandleData(lHandler, lData);
                    }
                    else
                    {
                        lHandler.BeginReceive(lStateObject.mBuffer, 0, StateObject.Buffer_Size, 0, new AsyncCallback(ReadCallback), lStateObject);
                    }
                }
                else
                {
                    Console.WriteLine("itt van valami.");
                }
            }
            catch
            {
                Console.WriteLine("Error detected!");
            }
        }

        private void SendCallback(IAsyncResult AR)
        {
            try
            {
                Console.WriteLine("SendCallBack");
                Socket lHandler = (Socket)AR.AsyncState;
                int lSentBytes = lHandler.EndSend(AR);
                mLastStatus = "Sent " + lSentBytes.ToString() + " bytes.";

//                lHandler.Shutdown(SocketShutdown.Both);
//                lHandler.Close();
                ESendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void CloseConnection(Socket pSocket)
        {
            try
            {
                pSocket.Shutdown(SocketShutdown.Both);
                pSocket.Close();
            }
            catch (SocketException ex)
            {
                ShowErrorDialog(ex.Message);
            }

        }

        // Protocol functions

        public void SendStyleList(Socket pSocket)
        {
            byte[] lProtocolVersion = BitConverter.GetBytes(PROTOCOL_VERSION);
            MemoryStream lMs = new MemoryStream();
            StreamWriter lSw = new StreamWriter(lMs);

            lSw.WriteLine((byte)Envelope.protocolVersion);
            lSw.WriteLine(PROTOCOL_VERSION);
            lSw.WriteLine((byte)Envelope.tweetStyleList);
            lSw.WriteLine(mTweetStyles.Count);
            foreach (var Style in mTweetStyles)
            {
                lSw.WriteLine(Style.styleName);
                lSw.WriteLine(Style.styleImageURL);
            }
            lSw.WriteLine(EOF_STRING);
            lSw.Flush();
            SendData(pSocket, lMs);
            ESendDone.WaitOne();
        }

        public void SendTweets(Socket pSocket,string pStyle)
        {
            UpdateTweetsWithStyle(pStyle);

            MemoryStream lMs = new MemoryStream();
            StreamWriter lSw = new StreamWriter(lMs);
            lSw.WriteLine((byte)Envelope.protocolVersion);
            lSw.WriteLine(PROTOCOL_VERSION);
            lSw.WriteLine((byte)Envelope.tweetTweets);
            lSw.WriteLine(mTweets.Count);
            foreach (var Tweet in mTweets)
            {
                Console.WriteLine(Tweet.Base64Decode(Tweet.TweetText));
                lSw.WriteLine(Tweet.TweetID);
                lSw.WriteLine(Tweet.TweetText);
                lSw.WriteLine(Tweet.TweetTimeStamp);
                lSw.WriteLine(Tweet.TweetUpvotes);
                lSw.WriteLine(Tweet.TweetDownvotes);
            }
            lSw.WriteLine(EOF_STRING);
            lSw.Flush();
            SendData(pSocket, lMs);
            ESendDone.WaitOne();
        }

        public void SendEOT(Socket pSocket)
        {
            MemoryStream lMs = new MemoryStream();
            StreamWriter lSw = new StreamWriter(lMs);
            lSw.WriteLine(Envelope.protocolVersion);
            lSw.WriteLine(PROTOCOL_VERSION);
            lSw.WriteLine(Envelope.tweetEOT);
            SendData(pSocket, lMs);
        }

        private void SendData(Socket pSocket, MemoryStream pMemoryStream)
        {
            byte[] lBuffer = pMemoryStream.ToArray();

            try
            {
                pSocket.BeginSend(lBuffer, 0, lBuffer.Length, SocketFlags.None, SendCallback, pSocket);
                ESendDone.WaitOne();
            }
            catch (SocketException ex)
            {
                ShowErrorDialog(ex.Message);
            }
            catch (ObjectDisposedException ex)
            {
                ShowErrorDialog(ex.Message);
            }
        }

        // Handling incoming data

        private void ServerHandleData(Socket pSocket, string pData)
        {
            ServerState lServerState = ServerState.stateStart;
            bool lProtocolVersionInOrder = false;

            using (StringReader reader = new StringReader(pData))
            {
                string lLine;
                while ((lLine = reader.ReadLine()) != null)
                {
                    switch (Byte.Parse(lLine))
                    {
                        case (byte)Envelope.protocolVersion:
                            {
                                if ((lLine = reader.ReadLine()) != null)
                                {
                                    if (0 == lLine.CompareTo(PROTOCOL_VERSION.ToString()))
                                    {
                                        lProtocolVersionInOrder = true;
                                    }
                                }
                                else
                                {
                                    lServerState = ServerState.stateProtocolError;
                                    mLastStatus = "Protocol version mismatch. Expecting " + PROTOCOL_VERSION.ToString() + " and got " + lLine;
                                    break;
                                }
                                break;
                            }
                        case (byte)Envelope.tweetStyleList:
                            {
                                if (!lProtocolVersionInOrder)
                                {
                                    lServerState = ServerState.stateProtocolError;
                                    mLastStatus = "TweetStyleList received before comparing protocol versions.";
                                    break;
                                }
                                else
                                {
                                    mLastStatus = "Received request for StyleList, sending it now.";
                                    SendStyleList(pSocket);
                                    return;
                                }
                            }
                        case (byte)Envelope.tweetTweets:
                            {
                                if (!lProtocolVersionInOrder)
                                {
                                    lServerState = ServerState.stateProtocolError;
                                    mLastStatus = "Tweets received before comparing protocol versions.";
                                }
                                else
                                {
                                    mLastStatus = "Received request for Tweets, sending it now.";
                                    if ((lLine = reader.ReadLine()) != null)
                                    {
                                        SendTweets(pSocket,lLine);
                                        return;
                                    }
                                    else
                                    {
                                        lServerState = ServerState.stateProtocolError;
                                        mLastStatus = "Protocol version mismatch. Expecting " + PROTOCOL_VERSION.ToString() + " and got " + lLine;
                                    }
                                }
                                break;
                            }
                        default:
                            {
                                lServerState = ServerState.stateProtocolError;
                                mLastStatus = "Received something I could not decode : \n" + pData + ". Closing connection.";
                                SendEOT(pSocket);
                                CloseConnection(pSocket);
                                break;
                            }
                    }
                    if (ServerState.stateProtocolError == lServerState)
                    {
                        break;
                    }
                }
            }
        }

        internal void UpdateTweetStyles(List<TweetStyle> pTweetStyles)
        {
            if (pTweetStyles != null)
                mTweetStyles = pTweetStyles;
        }

        private void UpdateTweetsWithStyle(string pStyleName)
        {
            List<Tweet> tweets = mOwner.sqlDBConnection.GetTweetsFromDatabase(TWEETS_TO_SEND);

            TweetStyle selectedStyle = null;
            foreach (TweetStyle style in mTweetStyles)
                if (style.styleName == pStyleName)
                {
                    selectedStyle = style;
                    break;
                }

            foreach (Tweet tweet in tweets)
            {
                string decodedTweet = Tweet.Base64Decode(tweet.TweetText);

                foreach (StyleProperty property in mOwner.GetStyleProperties(selectedStyle.styleName))
                {
                    // Exact search, narrow search pattern
                    string replacePattern = @"\b" + property.Original + @"\b";
                    string result = Regex.Replace(decodedTweet, replacePattern, property.Replacement, RegexOptions.IgnoreCase);

                    // Loose search, wide search pattern
                    //string replacePattern = property.Original; // Partial match
                    //string result = Regex.Replace(decodedTweet, replacePattern, property.Replacement, RegexOptions.IgnoreCase);

                    decodedTweet = result;
                }
                tweet.Updatetext(Tweet.Base64Encode(decodedTweet));
            }
            mTweets = tweets;
        }
    }
}