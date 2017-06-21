using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NiceHashMiner;

namespace NiceHashMiner.Web.Models
{
    public class GroupMinerStatus
    {
        public readonly string MinerTAG;
        public readonly string DevicesInfoString;
        public readonly APIData APIData;
        public readonly double CurrentRate;
        public readonly bool IsAPIReadException;

        public GroupMinerStatus(string minerTAG,
            string devicesInfoString,
            APIData apiData,
            double currentRate,
            bool isAPIReadException) {
            MinerTAG = minerTAG;
            DevicesInfoString = devicesInfoString;
            APIData = apiData;
            CurrentRate = currentRate;
            IsAPIReadException = isAPIReadException;
        }
    }
}
