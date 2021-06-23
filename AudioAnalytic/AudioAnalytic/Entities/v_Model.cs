﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioAnalytic.Entities
{
    public class v_Model
    {
        public int Result { get; set; } = 0;
        public string Gender { get; set; }
        public int Age { get; set; } = 0;
        public string Name { get; set; }


        public string TargetWav
        {
            get
            {
                return $"{Result}_{Age}_{Gender}_{Name}.wav";
            }
        }
        public string TargetSpec
        {
            get
            {
                return $"{Result}_{Age}_{Gender}_{Name}.png";
            }
        }
    }
}
