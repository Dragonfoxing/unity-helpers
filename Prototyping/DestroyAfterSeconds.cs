using System.Collections;
using UnityEngine;

namespace Dragonfoxing.UnityCSharp {
    
    public class DestroyAfterSeconds : MonoBehaviour
    {
        public float delayTimer = 3f;

        private void OnEnable()
        {
            StartCoroutine(ExecuteAfterTime(delayTimer));
        }

        private void OnDisable()
        {
            StopCoroutine(ExecuteAfterTime(delayTimer));
        }

        private void OnApplicationQuit()
        {
            StopCoroutine(ExecuteAfterTime(delayTimer));
        }

        private void OnDestroy()
        {
            StopCoroutine(ExecuteAfterTime(delayTimer));
        }

        IEnumerator ExecuteAfterTime(float n)
        {
            yield return new WaitForSeconds(n);
            Destroy(gameObject);
        }
    }
}
