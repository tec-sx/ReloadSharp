namespace Reload.Utilities.Assertions
{
    using System.Diagnostics;

    public class UnnecessaryLock
    {
        private volatile bool isLocked = false;

        [Conditional("DEBUG")]
        public void Aquire()
        {
            Debug.Assert(!isLocked);
            isLocked = true;
        }

        [Conditional("DEBUG")]
        public void Release()
        {
            Debug.Assert(isLocked);
            isLocked = false;
        }
    }
}
