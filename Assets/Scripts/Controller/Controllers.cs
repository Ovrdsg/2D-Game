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

        internal void Add(IController controller)
        {
            if(controller is IRemoveFromControllers controllerToRemove)
            {
                controllerToRemove.PlayerRemoved += Remove;
            }
            if(controller is IInitialization initializeController)
            {
                _initilalizeControllers.Add(initializeController);
            }
            if(controller is IExecute executeController)
            {
                _executeControllers.Add(executeController);
            }
            if(controller is ILateExecute lateExecuteController)
            {
                _lateExecuteControllers.Add(lateExecuteController);
            }
            if(controller is IFixedExecute fixedExecuteController)
            {
                _fixedExecuteControllers.Add(fixedExecuteController);
            }

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

        public void Execute(float deltaTime)
        {
            for(var index = 0; index <_executeControllers.Count; index++)
            {
                _executeControllers[index].Execute(deltaTime);
            }
        }

        public void LateExecute(float deltaTime)
        {
            for(var index = 0; index <_lateExecuteControllers.Count; index++)
            {
                _lateExecuteControllers[index].LateExecute(deltaTime);
            }
        }

        public void FixedExecute(float deltaTime)
        {
            for(var index = 0; index < _fixedExecuteControllers.Count; index++)
            {
                _fixedExecuteControllers[index].FixedExecute(deltaTime);
            }
        }
    }
}
