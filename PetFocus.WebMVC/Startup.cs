﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PetFocus.WebMVC.Startup))]
namespace PetFocus.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
