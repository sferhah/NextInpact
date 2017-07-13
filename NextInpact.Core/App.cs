using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.IoC;
using NextInpact.Core.Data;
using NextInpact.Core.Models;
using NextInpact.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextInpact.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            ThreadSafeSqlite.Instance.Init(typeof(Article), typeof(Comment));

            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<ArticlesViewModel>();            
        }
    }
}
