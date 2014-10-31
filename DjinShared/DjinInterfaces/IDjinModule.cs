namespace Djin.Shared.Interfaces
{

    public interface IDjinModule
    {
        /* will only be called once,
         * when the module is donwloaded
         */
        void Install();

        /* will only be called once,
         * when the module will be deleted
         */
        void Uninstall();

        /* Will be called every time the
         * instance of the
         * Module was created
         */
        void OnStart();

        /* Will be called every time
         * an instance of the 
         * Module will be destroyed
         */
        void OnStop();

        /* This is the "real" code which will be executed.
         * It will be called after OnStart() end before OnStop()
         */
        void Run();
    }
}
