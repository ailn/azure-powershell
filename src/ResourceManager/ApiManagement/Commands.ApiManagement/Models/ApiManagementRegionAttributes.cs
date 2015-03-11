﻿//  
// Copyright (c) Microsoft.  All rights reserved.
// 
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//    http://www.apache.org/licenses/LICENSE-2.0
// 
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
namespace Microsoft.Azure.Commands.ApiManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Azure.Management.ApiManagement.Models;

    public class ApiManagementRegionAttributes
    {
        public ApiManagementRegionAttributes(){}

        public ApiManagementRegionAttributes(AdditionalRegion regionResource)
            :this()
        {
            if (regionResource == null)
            {
                throw new ArgumentNullException("regionResource");
            }

            this.Location = regionResource.Location;
            this.Sku = regionResource.SkuType.ToString();
            this.Capacity = regionResource.SkuUnitCount ?? 1;
            this.StaticIPs = regionResource.StaticIPs.ToArray();

            if (regionResource.VirtualNetworkConfiguration != null)
            {
                this.VnetConfiguration = new VirtualNetworkConfigurationAttributes(regionResource.VirtualNetworkConfiguration);
            }
        }

        public VirtualNetworkConfigurationAttributes VnetConfiguration { get; set; }

        public string[] StaticIPs { get; private set; }

        public int Capacity { get; set; }

        public string Sku { get; set; }

        public string Location { get; set; }
    }
}