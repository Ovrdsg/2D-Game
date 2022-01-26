using System.Collections.Generic;
using UnityEngine;


namespace SunnyLand
{
    internal sealed class Controllers : IInitialization, IExecute, ILateExecute, IFixedExecute
    {
        private readonly List<IInitialization> _initilalizeControllers;
        private readonly List<IExecute> _executeControllers;
        private readonly List<ILateExecute> _lateExecuteControllers;
        private readonly List<IFixedExecute> _fixedExecuteControllers;


        internal Controllers()
        {
            _initilalizeControllers = new List<IInitialization>(8);
            _executeControllers = new List<IExecute>(8);
            _lateExecuteControllers = new List<ILateExecute>(8);
            _fixedExecuteControllers = new List<IFixedExecute>(8);
        }

        internal Controllers Add(IController controller)
        {
            if(controller is IRemoveFromControllers controllerToRemove)
            {
                controllerToRemove.PlayerRemoved += Remove;
            }
            if(controller is IInitialization initializeController)
            {
                _initilalizeControllers.Add(initializeController);
            }
            if(controller is IExecute executeContoller)
            {
                _executeControllers.Add(executeContoller);
            }
            if(controller is ILateExecute lateExecuteController)
            {
                _lateExecuteControllers.Add(lateExecuteController);
            }
            if(controller is IFixedExecute fixedExecuteController)
            {
                _fixedExecuteControllers.Add(fixedExecuteController);
            }

            return this;
        }

        internal void Remove(IController controller)
        {
            if(controller is IRemoveFromControllers controllerToRemove)
            {
                controllerToRemove.PlayerRemoved -= Remove;
            }

            if (controller is IInitialization initializeController)
            {
                _initilalizeControllers.Remove(initializeController);
            }
            if (controller is IExecute executeContoller)
            {
                _executeControllers.Remove(executeContoller);
            }
            if (controller is ILateExecute lateExecuteController)
            {
                _lateExecuteControllers.Remove(lateExecuteController);
            }
            if (controller is IFixedExecute fixedExecuteController)
            {
                _fixedExecuteControllers.Remove(fixedExecuteController);
            }
        }
        public void Initialization()
        {
            for(var index = 0; index < _initilalizeControllers.Count; index++)
            {
                _initilalizeControllers[index].Initialization();
            }
        }

        public void Execute(float deltatime)
        {
            for(var index = 0; index <_executeControllers.Count; index++)
            {
                _executeControllers[index].Execute(deltatime);
            }
        }

        public void LateExecute(float deltatime)
        {
            for(var index = 0; index <_lateExecuteControllers.Count; index++)
            {
                _lateExecuteControllers[index].LateExecute(deltatime);
            }
        }

        public void FixedExecute(float deltatime)
        {
            for(var index = 0; index < _fixedExecuteControllers.Count; index++)
            {
                _fixedExecuteControllers[index].FixedExecute(deltatime);
            }
        }
    }
}
