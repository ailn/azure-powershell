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
    using System.Management.Automation;
    using System.Threading;
    using Microsoft.Azure.Commands.ApiManagement.Models;
    using Microsoft.Azure.Commands.ApiManagement.Properties;
    using Microsoft.WindowsAzure.Commands.Utilities.Common;

    public class ApiManagementCmdletBase : AzurePSCmdlet
    {
        protected static TimeSpan LongRunningOperationDefaultTimeout = TimeSpan.FromMinutes(1);

        private ApiManagementClient _client;

        public ApiManagementClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new ApiManagementClient(Profile.Context);
                }
                return _client;
            }
            set
            {
                _client = value;
            }
        }

        protected void WriteProgress(ApiManagementLongRunningOperation operation)
        {
            WriteProgress(new ProgressRecord(0, operation.OperationName, operation.Status.ToString()));
        }

        protected ApiManagementLongRunningOperation WaitForOperationToComplete(ApiManagementLongRunningOperation longRunningOperation)
        {
            WriteProgress(longRunningOperation);

            while (longRunningOperation.Status == OperationStatus.InProgress)
            {
                var retryAfter = longRunningOperation.RetryAfter ?? LongRunningOperationDefaultTimeout;

                WriteVerboseWithTimestamp(Resources.VerboseGetOperationStateTimeoutMessage, retryAfter);
                Thread.Sleep(retryAfter);

                longRunningOperation = Client.GetLongRunningOperationStatus(longRunningOperation);
                WriteProgress(longRunningOperation);
            }

            return longRunningOperation;
        }

        protected void ExecuteCmdLetWrap(Action action)
        {
            try
            {
                action();
            }
            catch (ArgumentException ex)
            {
                WriteError(new ErrorRecord(ex, string.Empty, ErrorCategory.InvalidArgument, null));
            }
            catch (Exception ex)
            {
                WriteExceptionError(ex);
            }
        }
    }
}