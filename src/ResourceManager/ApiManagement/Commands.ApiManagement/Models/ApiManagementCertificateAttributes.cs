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
    using Microsoft.Azure.Management.ApiManagement.Models;

    public class ApiManagementCertificateAttributes
    {
        public ApiManagementCertificateAttributes()
        {
        }

        public ApiManagementCertificateAttributes(ApiManagementHostnameType hostnameType, CertificateInformation value)
            :this()
        {
            this.HostnameType = hostnameType;
            this.Expiry = value.Expiry;
            this.Subject = value.Subject;
            this.Thumbprint = value.Thumbprint;
        }

        public ApiManagementHostnameType HostnameType { get; set; }

        public string Thumbprint { get; set; }

        public string Subject { get; set; }

        public DateTime Expiry { get; set; }
    }
}