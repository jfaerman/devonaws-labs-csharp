﻿// Copyright 2013 Amazon.com, Inc. or its affiliates. All Rights Reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License"). You may not 
// use this file except in compliance with the License. A copy of the License 
// is located at
// 
// 	http://aws.amazon.com/apache2.0/
// 
// or in the "LICENSE" file accompanying this file. This file is distributed 
// on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either 
// express or implied. See the License for the specific language governing 
// permissions and limitations under the License.

using System;
using Amazon.Runtime;

namespace AwsLabs
{
    /// <summary>
    ///     Class used for pulling credentials from system environment variables AWS_ACCESS_KEY_ID and AWS_SECRET_KEY.
    /// </summary>
    public class SystemEnvironmentAWSCredentials : AWSCredentials
    {
        private readonly ImmutableCredentials _credentials;

        public SystemEnvironmentAWSCredentials()
        {
            string accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID", EnvironmentVariableTarget.User);
            if (String.IsNullOrEmpty(accessKey))
            {
                accessKey = Environment.GetEnvironmentVariable("AWSAccessKey", EnvironmentVariableTarget.User);
            }

            string secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_KEY", EnvironmentVariableTarget.User);
            if (String.IsNullOrEmpty(secretKey))
            {
                secretKey = Environment.GetEnvironmentVariable("AWSSecretKey", EnvironmentVariableTarget.User);
            }

            if (String.IsNullOrEmpty(accessKey) || String.IsNullOrEmpty(secretKey))
            {
                throw new Exception("No credentials found in the system environment.");
            }
            _credentials = new ImmutableCredentials(accessKey, secretKey, "", true);
        }

        public override ImmutableCredentials GetCredentials()
        {
            return _credentials != null ? _credentials.Copy() : null;
        }
    }
}
