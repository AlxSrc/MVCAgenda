using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCAgenda.Core.Logging
{
    public enum LogInfo
    {
        /// <summary>
        /// Create
        /// </summary>
        Create = 10,

        /// <summary>
        /// Read
        /// </summary>
        Read = 20,

        /// <summary>
        /// Edit
        /// </summary>
        Edit = 30,

        /// <summary>
        /// Delete
        /// </summary>
        Delete = 40,

        /// <summary>
        /// Hide
        /// </summary>
        Hide = 50
    }
}