using EnvironmentChanger.Redirection;

namespace EnvironmentChanger.Detours
{
    public class LoadSavePanelBaseType : TargetTypeAttribute
    {
        public LoadSavePanelBaseType() :
            base(typeof(LoadSavePanelBase<>).MakeGenericType(typeof(MetaData)))
        {
        }
    }
}
