using System.Threading;
using UnityEditor;
using UnityEngine;

namespace Unity.Services.CCD.Management.Scheduler
{
    /// <summary>
    /// Thread helper
    /// </summary>
    // "inspired" by UniTask
    public static class ThreadHelper
    {
        /// <summary>
        /// Synchronization Context
        /// </summary>
        public static SynchronizationContext SynchronizationContext => _unitySynchronizationContext;
        /// <summary>
        /// Task Scheduler
        /// </summary>
        public static System.Threading.Tasks.TaskScheduler TaskScheduler => _taskScheduler;
        /// <summary>
        /// Main Thread Id
        /// </summary>
        public static int MainThreadId => _mainThreadId;

        private static SynchronizationContext _unitySynchronizationContext;
        private static System.Threading.Tasks.TaskScheduler _taskScheduler;
        private static int _mainThreadId;

        /// <summary>
        /// Init Task Scheduler
        /// </summary>
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
#endif
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Init()
        {
            _unitySynchronizationContext = SynchronizationContext.Current;
            _taskScheduler = System.Threading.Tasks.TaskScheduler.FromCurrentSynchronizationContext();
            _mainThreadId = Thread.CurrentThread.ManagedThreadId;
        }
    }
}
