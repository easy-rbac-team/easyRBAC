namespace EasyRbac.Reponsitory.Helper
{
    public interface IKeyedIdGenerate
    {
        int NewId(string key);
    }
}