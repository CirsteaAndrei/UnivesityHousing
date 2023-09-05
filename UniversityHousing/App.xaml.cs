using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using UniversityHousing.Helpers;
using UniversityHousing.Models;
using UniversityHousing.Models.Repositories;

namespace UniversityHousing
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            services.AddSingleton<AppDbContext>();
            services.AddSingleton<UnitOfWork>();
            services.AddTransient<StudentsRepository>();
            services.AddTransient<DormitoriesRepository>();
            services.AddTransient<PaymentsRepository>();
            services.AddTransient<PenaltiesRepository>();
            services.AddTransient<RoomsRepository>();

            ServiceLocator.Initialize(services);
        }
    }
}
