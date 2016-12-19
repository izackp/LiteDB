﻿using System;

namespace LiteDB
{
    /// <summary>
    /// Datafile open options (for FileDiskService)
    /// </summary>
    public class FileOptions
    {
        public bool Journal { get; set; }
        public long InitialSize { get; set; }
        public long LimitSize { get; set; }
        public TimeSpan Timeout { get; set; }
        public FileMode FileMode { get; set; }

        public FileOptions()
        {
            this.Journal = true;
            this.InitialSize = BasePage.PAGE_SIZE;
            this.LimitSize = long.MaxValue;
            this.Timeout = TimeSpan.FromMinutes(1);
            this.FileMode = FileMode.Shared;
        }
    }

    public enum FileMode
    {
        Shared,
        ReadOnly,
        Exclusive
    }
}
