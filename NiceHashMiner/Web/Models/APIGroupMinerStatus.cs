using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NiceHashMiner.Enums;
using NiceHashMiner;

namespace NiceHashMiner.Web.Models
{
    public class APIGroupMinerStatus
    {
        public AlgorithmType NiceHashID;
        public string AlgorithmName;
        public double Speed;
        public AlgorithmType SecondaryNiceHashID;
        public string SecondaryAlgorithmName = "";
        public double SecondarySpeed;
        public double Rate;
        public string DeviceString;

        public APIGroupMinerStatus(APIData apiData, double rate) {
            NiceHashID = apiData.AlgorithmID;
            AlgorithmName = apiData.AlgorithmName;
            Speed = apiData.Speed;
            SecondaryNiceHashID = apiData.SecondaryAlgorithmID;
            SecondarySpeed = apiData.SecondarySpeed;
            Rate = rate;
        }
    }
}
