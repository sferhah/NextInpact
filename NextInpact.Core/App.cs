using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.IoC;
using NextInpact.Core.Data;
using NextInpact.Core.Models;
using NextInpact.Core.ViewModels;

namespace NextInpact.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            AppDbContext.Init();

            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
            RegisterNavigationServiceAppStart<ArticlesViewModel>();            
        }
    }
}
