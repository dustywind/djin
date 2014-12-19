namespace Djin.Shared.Interfaces
{

    public interface IDjinModule
    {

        /**
         * About _Constructors_:
         * There are four different types of Constructors, that are valid for IDjinModules
         * 1. IDjinModule();
         * 2. IDjinModule(IDjinOutput)
         * 3. IDjinModule(Dictionary<string, string>);
         * 4. IDjinModule(IDjinOutput, Dictionary<string, string>);
         *
         * Everything else will be ignored.
         * Depending on wheter the given parameters can be filled or not,
         * the best matching constructor will be chosen.
         */


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
