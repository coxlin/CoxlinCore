using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using UnityEngine;

namespace CoxlinCore
{
   
    public static class Extensions
    {
        #region Action Extensions

        public static void Call(this Action action)
        {
            action?.Invoke();
        }

        public static void RemoveAllSubscribedActions(this Action action)
        {
            if (action == null) return;
            
            foreach (var handler in action.GetInvocationList())
            {
                action -= handler as Action;
            }
        }

        public static bool IsEventHandlerRegistered(
            this Delegate eventHandler,
            Delegate prospectiveHandler)
        {
            return eventHandler != null && 
                   eventHandler.GetInvocationList().Any(existingHandler => existingHandler == prospectiveHandler);
        }

        #endregion

        #region Animator Extensions

        public static bool AnimatorStateIsFinished(
            this Animator animator, string stateName, int layerIndex = 0)
        {
            if (animator == null) return false;
            
            AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);
            return animatorStateInfo.normalizedTime >= 1 && animatorStateInfo.IsName(stateName);
        }
        
        public static bool CurrentStateHasName(this Animator animator, string name, int layerIndex)
        {
            if (animator == null) return false;
            
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);
            int checkHash = Animator.StringToHash(name);
            return stateInfo.shortNameHash == checkHash;
        }
        
        public static void CrossFadeToStateIfNameIsDifferent(
            this Animator animator,
            string name,
            int layerIndex)
        {
            if (animator == null) return;
            
            if (!animator.CurrentStateHasName(name, layerIndex))
            {
                animator.CrossFade(name, 0f);
            }
        }

        #endregion

        #region AnimationCurve Extensions

        public static void SetValueForKey(this AnimationCurve curve, float time, float value)
        {
            if (curve == null) return;
            
            for (int i = 0; i < curve.keys.Length; ++i)
            {
                if (!Mathf.Approximately(curve.keys[i].time, time)) continue;
                
                curve.RemoveKey(i);
                break;
            }
            curve.AddKey(time, value);
        }

        #endregion

        #region Array Extensions

        public static T Find<T>(this T[] array, Predicate<T> predicate)
        {
            return Array.Find(array, predicate);
        }

        public static bool IndexIsAtOrGreaterThanLastIndex<T>(this T[] array, int index)
        {
            return array == null || index >= array.Length;
        }

        public static void ResetIndexIfItIsAtOrGreaterThanLastIndex<T>(this T[] array, ref int index)
        {
            if (array == null || IndexIsAtOrGreaterThanLastIndex(array, index))
            {
                index = 0;
            }
        }

        public static T First<T>(this T[] array)
        {
            if (array == null || array.Length == 0)
                throw new InvalidOperationException("Cannot get first element of empty or null array");
                
            return array[0];
        }

        public static T Last<T>(this T[] array)
        {
            if (array == null || array.Length == 0)
                throw new InvalidOperationException("Cannot get last element of empty or null array");
                
            return array[^1];
        }

        public static T[] SetAllValues<T>(this T[] array, T value)
        {
            if (array == null) return array;
            
            for (int i = 0; i < array.Length; ++i)
            {
                array[i] = value;
            }
            return array;
        }

        public static bool IndexIsInRange<T>(this T[] array, int index)
        {
            return array != null && index >= 0 && index < array.Length;
        }

        public static ReadOnlyCollection<T> ToReadOnly<T>(this T[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
                
            return new ReadOnlyCollection<T>(array);
        }

        public static T[] Flatten<T>(this T[,] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
                
            return array.Cast<T>().ToArray();
        }

        public static T[,] To2D<T>(this T[] flatArray, int width)
        {
            if (flatArray == null)
                throw new ArgumentNullException(nameof(flatArray));
            if (width <= 0)
                throw new ArgumentException("Width must be positive", nameof(width));
            
            int height = (int)Math.Ceiling(flatArray.Length / (double)width);
            T[,] result = new T[height, width];
            
            for (int index = 0; index < flatArray.Length; index++)
            {
                int rowIndex = index / width;
                int colIndex = index % width;
                result[rowIndex, colIndex] = flatArray[index];
            }
            
            return result;
        }

        #endregion

        #region Collection Extensions

        public static T First<T>(this IList<T> list)
        {
            if (list == null || list.Count == 0)
                throw new InvalidOperationException("Cannot get first element of empty or null list");
                
            return list[0];
        }

        public static T Last<T>(this IList<T> list)
        {
            if (list == null || list.Count == 0)
                throw new InvalidOperationException("Cannot get last element of empty or null list");
                
            return list[^1];
        }

        public static bool IsNullOrEmpty<T>(this IList<T> list)
        {
            return list == null || list.Count == 0;
        }

        public static bool IsNotNullOrEmpty<T>(this IList<T> list)
        {
            return list != null && list.Count > 0;
        }

        public static IList<T> SetAllValues<T>(this IList<T> list, T value)
        {
            if (list == null) return list;
            
            for (int i = 0; i < list.Count; ++i)
            {
                list[i] = value;
            }
            return list;
        }

        public static bool HasXElements<T>(this IEnumerable<T> source, int elementAmount)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
                
            return source.Count() == elementAmount;
        }

        public static bool IndexIsInRange<T>(this List<T> list, int index)
        {
            return list != null && index >= 0 && index < list.Count;
        }

        public static ReadOnlyCollection<T> ToReadOnly<T>(this IList<T> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
                
            return new ReadOnlyCollection<T>(list);
        }

        public static void Initialize<T>(this List<T> currentList, List<T> sourceList)
        {
            if (currentList == null)
                throw new ArgumentNullException(nameof(currentList));
            if (sourceList == null)
                throw new ArgumentNullException(nameof(sourceList));
                
            currentList.Clear();
            currentList.AddRange(sourceList);
        }

        public static void EnqueueAll<T>(this Queue<T> queue, ICollection<T> collection)
        {
            if (queue == null)
                throw new ArgumentNullException(nameof(queue));
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
                
            foreach (var item in collection)
            {
                queue.Enqueue(item);
            }
        }

        public static void EnqueueAll<T>(this Queue<T> queue, Queue<T> q)
        {
            while (q.Count > 0)
            {
                queue.Enqueue(q.Dequeue());
            }
        }

        public static bool ContainsAll<T>(this HashSet<T> hashset, ICollection<T> collection)
        {
            if (hashset == null)
                throw new ArgumentNullException(nameof(hashset));
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
                
            return collection.All(hashset.Contains);
        }

        public static List<List<T>> ChunkBy<T>(this List<T> source, int chunkSize)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (chunkSize <= 0)
                throw new ArgumentException("Chunk size must be positive", nameof(chunkSize));
                
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }

        public static List<T> GetChunk<T>(this List<T> source, int startIndex, int endIndex)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (startIndex < 0 || startIndex >= source.Count)
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (endIndex < startIndex || endIndex > source.Count)
                throw new ArgumentOutOfRangeException(nameof(endIndex));
                
            var list = new List<T>();
            for (int i = startIndex; i < endIndex; ++i)
            {
                list.Add(source[i]);
            }
            return list;
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
                
            var random = new System.Random();
            return source.OrderBy(_ => random.Next());
        }

        #endregion

        #region DateTime Extensions

        public static int ToUnixTimestamp(this DateTime dateTime)
        {
            TimeSpan span = dateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return (int)span.TotalSeconds;
        }
        
        public static bool IsWeekend(this DateTime dateTime)
        {
            return dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday;
        }
        
        public static bool IsLeapYear(this DateTime dateTime)
        {
            return DateTime.IsLeapYear(dateTime.Year);
        }
        
        public static bool Between(this DateTime dateTime, DateTime rangeStart, DateTime rangeEnd)
        {
            return dateTime.Ticks >= rangeStart.Ticks && dateTime.Ticks <= rangeEnd.Ticks;
        }

        public static DateTime NextDayOfTheWeek(this DateTime current, DayOfWeek dayOfWeek)
        {
            int offsetDays = dayOfWeek - current.DayOfWeek;
            if (offsetDays <= 0)
            {
                offsetDays += 7;
            }
            return current.AddDays(offsetDays);
        }

        #endregion

        #region Number Extensions

        public static bool IsInRange(this double value, double start, double end)
        {
            return value >= start && value <= end;
        }
        
        public static bool IsInRange(this float value, float start, float end)
        {
            return value >= start && value <= end;
        }
        
        public static float ConvertToNegative(this float value)
        {
            return -Mathf.Abs(value);
        }
        
        public static bool IsInRange(this int value, int start, int end)
        {
            return value >= start && value <= end;
        }

        #endregion

        #region GameObject Extensions

        public static GameObject AddChild(this GameObject parent, GameObject prefab)
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent));
            if (prefab == null)
                throw new ArgumentNullException(nameof(prefab));
                
            GameObject gameObject = GameObject.Instantiate(prefab);
            if (gameObject != null)
            {
                Transform transform = gameObject.transform;
                transform.SetParentAndCleanTransform(parent.transform);
                gameObject.layer = parent.layer;
            }
            return gameObject;
        }
        
        public static void SetLayerRecursively(this GameObject gameObject, int layer)
        {
            if (gameObject == null) return;
            
            gameObject.layer = layer;
            foreach (Transform child in gameObject.transform)
            {
                SetLayerRecursively(child.gameObject, layer);
            }
        }
        
        public static List<GameObject> GetChildObjectsWithTag(this GameObject gameObject, string tag)
        {
            if (gameObject == null)
                throw new ArgumentNullException(nameof(gameObject));
            if (string.IsNullOrEmpty(tag))
                throw new ArgumentException("Tag cannot be null or empty", nameof(tag));
                
            var childObjects = new List<GameObject>();
            foreach (Transform transform in gameObject.GetComponentsInChildren<Transform>())
            {
                if (transform.gameObject.CompareTag(tag))
                {
                    childObjects.Add(transform.gameObject);
                }
            }
            return childObjects;
        }

        #endregion

        #region Quaternion Extensions

        public static Quaternion Inverse(this Quaternion quaternion)
        {
            return Quaternion.Inverse(quaternion);
        }

        public static Quaternion QuaternionFromMatrix(Matrix4x4 matrix)
        {
            return Quaternion.LookRotation(matrix.GetColumn(2), matrix.GetColumn(1));
        }

        public static Quaternion RotateInSpecifiedCoordinateSystem(Quaternion rotation, Transform referenceCoordSys)
        {
            if (referenceCoordSys == null)
                throw new ArgumentNullException(nameof(referenceCoordSys));
                
            return referenceCoordSys.rotation * rotation * referenceCoordSys.rotation.Inverse();
        }

        public static float Pitch(this Quaternion quaternion)
        {
            return Mathf.Atan2(
                2f * (quaternion.y * quaternion.z + quaternion.w * quaternion.x), 
                quaternion.w * quaternion.w - quaternion.x * quaternion.x - quaternion.y * quaternion.y + quaternion.z * quaternion.z);
        }

        public static float Yaw(this Quaternion quaternion)
        {
            return Mathf.Asin(-2f * (quaternion.x * quaternion.z - quaternion.w * quaternion.y));
        }

        public static float Roll(this Quaternion quaternion)
        {
            return Mathf.Atan2(
                2f * (quaternion.x * quaternion.y + quaternion.w * quaternion.z), 
                quaternion.w * quaternion.w + quaternion.x * quaternion.x - quaternion.y * quaternion.y - quaternion.z * quaternion.z);
        }

        #endregion

        #region SpriteRenderer Extensions

        public static void SetAlpha(this SpriteRenderer spriteRenderer, float alpha)
        {
            if (spriteRenderer == null) return;
            
            var color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }

        #endregion

        #region String Extensions

        public static string UppercaseFirst(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            
            return char.ToUpper(text[0]) + text.Substring(1);
        }

        public static string LowercaseFirst(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            
            return char.ToLowerInvariant(text[0]) + text.Substring(1);
        }

        public static string FormatString(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static string Copy(this string text)
        {
            return string.IsNullOrEmpty(text) ? string.Empty : string.Copy(text);
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        #endregion

        #region Texture Extensions

        public static Texture2D ToTex2D(this RenderTexture renderTexture)
        {
            if (renderTexture == null)
                throw new ArgumentNullException(nameof(renderTexture));
                
            var texture2D = new Texture2D(
                renderTexture.width, 
                renderTexture.height,
                TextureFormat.ARGB32, 
                false);
                
            Graphics.CopyTexture(renderTexture, texture2D);
            return texture2D;
        }

        public static Texture2D ReadRenderTex(this RenderTexture renderTexture)
        {
            if (renderTexture == null)
                throw new ArgumentNullException(nameof(renderTexture));
                
            var texture2D = new Texture2D(
                renderTexture.width,
                renderTexture.height,
                TextureFormat.ARGB32,
                false);
                
            RenderTexture.active = renderTexture;
            texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture2D.Apply();
            RenderTexture.active = null;
            
            return texture2D;
        }

        public static Sprite ToSprite(this Texture2D texture)
        {
            if (texture == null)
                throw new ArgumentNullException(nameof(texture));
                
            Rect rect = new Rect(0, 0, texture.width, texture.height);
            return Sprite.Create(texture, rect, Vector2.zero);
        }

        public static Sprite ToSprite(this RenderTexture renderTexture)
        {
            if (renderTexture == null)
                throw new ArgumentNullException(nameof(renderTexture));
                
            var texture2D = renderTexture.ToTex2D();
            texture2D.filterMode = FilterMode.Point;
            texture2D.anisoLevel = 0;
            return texture2D.ToSprite();
        }

        public static void ToFile(this Texture2D texture, string path)
        {
            if (texture == null)
                throw new ArgumentNullException(nameof(texture));
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Path cannot be null or empty", nameof(path));
                
            byte[] bytes = texture.EncodeToPNG();
            File.WriteAllBytes(path, bytes);
        }

        #endregion

        #region Transform Extensions

        public static RectTransform AsRectTransform(this Transform transform) => (RectTransform)transform;
        
        public static RectTransform RectTransform(this GameObject gameObject) => 
            gameObject?.transform.AsRectTransform();
            
        public static RectTransform RectTransform<T>(this T component) where T : Component => 
            component?.transform.AsRectTransform();
            
        public static void LookAtOnlyUsingYRot(this Transform transform, Transform target)
        {
            if (transform == null || target == null) return;
            
            float x = transform.localEulerAngles.x;
            float z = transform.localEulerAngles.z;
            transform.LookAt(target);
            
            Vector3 euler = transform.localEulerAngles;
            euler.x = x;
            euler.z = z;
            transform.localEulerAngles = euler;
        }

        public static Transform[] GetChildTransforms(this GameObject gameObject)
        {
            if (gameObject == null)
                throw new ArgumentNullException(nameof(gameObject));
                
            var childTransforms = new List<Transform>(gameObject.GetComponentsInChildren<Transform>());
            var parentTransform = childTransforms.Find(x => x == gameObject.transform);
            if (parentTransform != null)
            {
                childTransforms.Remove(parentTransform);
            }
            return childTransforms.ToArray();
        }

        public static void DestroyChildren(this Transform parent)
        {
            if (parent == null) return;
            
            foreach (Transform child in parent)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        public static void SetParentAndZeroLocalPos(this Transform transform, Transform parent)
        {
            if (transform == null) return;
            
            transform.parent = parent;
            transform.localPosition = Vector3.zero;
        }

        public static void SetParentAndCleanTransform(this Transform transform, Transform parent)
        {
            if (transform == null) return;
            
            transform.parent = parent;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        public static int CountActiveChildren(this Transform transform)
        {
            if (transform == null) return 0;
            
            int count = 0;
            foreach (Transform child in transform)
            {
                if (child.gameObject.activeSelf)
                {
                    count++;
                }
            }
            return count;
        }

        #endregion

        #region Vector Extensions

        public static Vector3 Sum(this Vector3[] vectors)
        {
            if (vectors == null || vectors.Length == 0)
                return Vector3.zero;
                
            return vectors.Aggregate(Vector3.zero, (current, vec) => current + vec);
        }

        #endregion
    }
}