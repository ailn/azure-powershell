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
//

namespace Microsoft.Azure.Commands.ApiManagement.Commands
{
    using System.Management.Automation;
    using Microsoft.Azure.Commands.ApiManagement.Models;

    [Cmdlet(VerbsData.Update, "AzureApiManagementRegion"), OutputType(typeof(ApiManagement))]
    public class UpdateAzureApiManagementRegion : AzureApiManagementCmdletBase
    {
        [Parameter(
          ValueFromPipeline = true,
          Mandatory = true,
          HelpMessage = "ApiManagement object returned by Get-AzureApiManagement.")]
        [ValidateNotNull]
        public ApiManagement ApiManagement { get; set; }

        [Parameter(
            ValueFromPipelineByPropertyName = true,
            Mandatory = true, 
            HelpMessage = "Location of the region to update.")]

        [ValidateNotNullOrEmpty]
        [ValidateSet(
            "North Central US", "South Central US", "Central US", "West Europe", "North Europe", "West US", "East US",
            "East US 2", "Japan East", "Japan West", "Brazil South", "Southeast Asia", "East Asia", "Australia East",
            "Australia Southeast", IgnoreCase = false)]
        public string Location { get; set; }

        [Parameter(
            ValueFromPipelineByPropertyName = true,
            Mandatory = true,
            HelpMessage = "New tier value for region. Valid values are Developer, Standard and Premium.")]
        public ApiManagementSku Sku { get; set; }

        [Parameter(
            ValueFromPipelineByPropertyName = true,
            Mandatory = true,
            HelpMessage = "New Sku capacity value for region.")]
        public int Capacity { get; set; }

        [Parameter(
            ValueFromPipelineByPropertyName = true,
            Mandatory = false,
            HelpMessage = "Virtual network configuration. Default value is $null.")]
        public ApiManagementVirtualNetwork VirtualNetwork { get; set; }

        public override void ExecuteCmdlet()
        {
            ExecuteCmdLetWrap(() =>
            {
                ApiManagement.UpdateRegion(Location, Sku, Capacity, VirtualNetwork);

                WriteObject(ApiManagement);
            });
        }
    }
}