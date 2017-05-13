using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectServer
{
    /// <summary>
    /// This class represents a single cell of the database.
    /// </summary>
    public class DataHolder
    {
        public readonly string original;
        public readonly string replacement;

        /// <param name="pOriginal">Original word in the database</param>
        /// <param name="pReplacement">Replacement word in the database</param>
        public DataHolder(string pOriginal, string pReplacement)
        {
            this.original = pOriginal;
            this.replacement = pReplacement;
        }

        public override string ToString()
        {
            return "Original: " + this.original + ", Repalcement: " + this.replacement;
        }
    }
}
