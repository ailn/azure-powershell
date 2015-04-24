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
    using System;
    using System.Collections.Generic;
    using System.Management.Automation;
    using Microsoft.Azure.Commands.ApiManagement.Models;

    [Cmdlet(VerbsData.Update, "AzureApiManagementDeployment", DefaultParameterSetName = DefaultParameterSetName), OutputType(typeof(PsApiManagement))]
    public class UpdateAzureApiManagementDeployment : AzureApiManagementCmdletBase
    {
        internal const string FromApiManagementInstanceSetName = "Set from ApiManagement instance";
        internal const string DefaultParameterSetName = "Specific API Management service";

        [Parameter(
            ParameterSetName = FromApiManagementInstanceSetName,
            ValueFromPipeline = true,
            Mandatory = true,
            HelpMessage = "ApiManagementAttributes returned by Get-AzureApiManagement. Use Sku, Capacity, VirtualNetwork and " +
                          "AdditionalRegions properties to manage deployments.")]
        [ValidateNotNull]
        public PsApiManagement ApiManagement { get; set; }

        [Parameter(
            ParameterSetName = DefaultParameterSetName,
            ValueFromPipelineByPropertyName = true,
            Mandatory = true,
            HelpMessage = "Name of resource group of the API Management service.")]
        public string ResourceGroupName { get; set; }

        [Parameter(
            ParameterSetName = DefaultParameterSetName, 
            ValueFromPipelineByPropertyName = true, 
            Mandatory = true, 
            HelpMessage = "Name of API Management service.")]
        public string Name { get; set; }

        [Parameter(
            ParameterSetName = DefaultParameterSetName, 
            ValueFromPipelineByPropertyName = true, 
            Mandatory = true, 
            HelpMessage = "Location of master API Management service deployment.")]
        [ValidateSet(
            "North Central US", "South Central US", "Central US", "West Europe", "North Europe", "West US", "East US",
            "East US 2", "Japan East", "Japan West", "Brazil South", "Southeast Asia", "East Asia", "Australia East",
            "Australia Southeast", IgnoreCase = false)]
        public string Location { get; set; }

        [Parameter(
            ParameterSetName = DefaultParameterSetName,
            ValueFromPipelineByPropertyName = true,
            Mandatory = true,
            HelpMessage = "The tier of the Azure API Management service. Valid values are Developer, Standard and Premium .")]
        public PsApiManagementSku Sku { get; set; }

        [Parameter(
            ParameterSetName = DefaultParameterSetName,
            ValueFromPipelineByPropertyName = true,
            Mandatory = true,
            HelpMessage = "Sku capacity of the Azure API Management service.")]
        public int Capacity { get; set; }

        [Parameter(
            ParameterSetName = DefaultParameterSetName,
            ValueFromPipelineByPropertyName = true,
            Mandatory = false,
            HelpMessage = "Virtual Network Configuration of master Azure API Management service deployment.")]
        public PsApiManagementVirtualNetwork VirtualNetwork { get; set; }

        [Parameter(
            ParameterSetName = DefaultParameterSetName,
            ValueFromPipelineByPropertyName = true,
            Mandatory = false,
            HelpMessage = "Additional deployment regions of Azure API Management service.")]
        public IList<PsApiManagementRegion> AdditionalRegions { get; set; }

        [Parameter(
            Mandatory = false,
            HelpMessage = "Sends updated PsApiManagement to pipeline if operation succeeds.")]
        public SwitchParameter PassThru { get; set; }

        public override void ExecuteCmdlet()
        {
            string resourceGroupName, name, location;
            PsApiManagementSku sku;
            int capacity;
            PsApiManagementVirtualNetwork virtualNetwork;
            IList<PsApiManagementRegion> additionalRegions;

            if (ParameterSetName.Equals(DefaultParameterSetName, StringComparison.OrdinalIgnoreCase))
            {
                resourceGroupName = ResourceGroupName;
                name = Name;
                location = Location;
                sku = Sku;
                capacity = Capacity;
                virtualNetwork = VirtualNetwork;
                additionalRegions = AdditionalRegions;
            }
            else if (ParameterSetName.Equals(FromApiManagementInstanceSetName, StringComparison.OrdinalIgnoreCase))
            {
                resourceGroupName = ApiManagement.ResourceGroupName;
                name = ApiManagement.Name;
                location = ApiManagement.Location;
                sku = ApiManagement.Sku;
                capacity = ApiManagement.Capacity;
                virtualNetwork = ApiManagement.VirtualNetwork;
                additionalRegions = ApiManagement.AdditionalRegions;
            }
            else
            {
                throw new Exception(string.Format("Unrecongnized parameter set: {0}", ParameterSetName));
            }

            ExecuteLongRunningCmdletWrap(
                () => Client.BeginUpdateDeployments(
                    resourceGroupName,
                    name,
                    location,
                    sku,
                    capacity,
                    virtualNetwork,
                    additionalRegions),
                PassThru.IsPresent
                );
        }
    }
}