﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.OrderEntities
{
    public enum OrderStatus
    {
        [EnumMember(Value ="pending")]
        pending,
        [EnumMember(Value ="paymentSucceeded")]
        paymentSucceeded,
        [EnumMember(Value ="paymentFailed")]
        paymentFailed,
    }
}