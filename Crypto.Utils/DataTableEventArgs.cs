// ***********************************************************************
// Assembly         : Crypto.Utils
// Author           : mcarlucci
// Created          : 03-11-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-11-2018
// ***********************************************************************
// <copyright file="JsonEtl.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Data;

namespace Crypto.Utils
{
    public class DataTableEventArgs : EventArgs
    {
        public string Key { get; set; }
        public DataTable Table { get; set; }

        public DataTableEventArgs(DataTable table, string key)
        {
            this.Key = key;
            this.Table = table;
        }
    }
}