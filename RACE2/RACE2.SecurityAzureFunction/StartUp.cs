using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using RACE2.SecurityAzureFunction;

[assembly: WebJobsStartup(typeof(StartUp))]

public class StartUp : IWebJobsStartup
{
    public void Configure(IWebJobsBuilder builder)
    {


    }
}