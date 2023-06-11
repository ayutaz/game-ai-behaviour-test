using UnityEngine;

namespace Util
{
    public static class ColorUtil
    {
        /// <summary>
        /// HSV を RGB に変換する
        /// </summary>
        /// <param name="h">0-359</param>
        /// <param name="s">0-100</param>
        /// <param name="v">0-100</param>
        /// <returns>RGB(0～1) エラーの場合、全て 0</returns>
        public static (float R, float G, float B) HSVtoRGB(float h, float s, float v)
        {
            if (h is < 0.0f or >= 360.0f)
            {
                Debug.LogError($"[H] out of range] {h}");
                return (0, 0, 0);
            }

            if (s is < 0.0f or > 100.0f)
            {
                Debug.LogError($"[S] out of range] {s}");
                return (0, 0, 0);
            }

            if (v is < 0.0f or > 100.0f)
            {
                Debug.LogError($"[V] out of range] {v}");
                return (0, 0, 0);
            }

            var hmax = v;
            var hmin = hmax - ((s / 255) * hmax);
            float r = 0;
            float g = 0;
            float b = 0;

            s = s / 100.0f;
            v = v / 100.0f;

            if (s == 0)
            {
                r = v;
                g = v;
                b = v;
            }
            else
            {
                var dh = Mathf.Floor(h / 60.0f);
                var p = v * (1 - s);
                var q = v * (1 - s * (h / 60.0f - dh));
                var t = v * (1 - s * (1 - (h / 60.0f - dh)));

                switch (dh)
                {
                    case 0:
                        r = v;
                        g = t;
                        b = p;
                        break;
                    case 1:
                        r = q;
                        g = v;
                        b = p;
                        break;
                    case 2:
                        r = p;
                        g = v;
                        b = t;
                        break;
                    case 3:
                        r = p;
                        g = q;
                        b = v;
                        break;
                    case 4:
                        r = t;
                        g = p;
                        b = v;
                        break;
                    default:
                        r = v;
                        g = p;
                        b = q;
                        break;
                }
            }

            return (r, g, b);
        }

        /// <summary>
        /// RGB を HSV に変換する
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns>H(0-359), S(0-100), V(0-100) エラーの場合、全て 0</returns>
        public static (int H, int S, int V) RGBtoHSV(float r, float g, float b)
        {
            if (r is < 0.0f or > 1.0f)
            {
                Debug.LogError($"[R] out of range] {r}");
                return (0, 0, 0);
            }

            if (g is < 0.0f or > 1.0f)
            {
                Debug.LogError($"[G] out of range] {g}");
                return (0, 0, 0);
            }

            if (b is < 0.0f or > 1.0f)
            {
                Debug.LogError($"[B] out of range] {b}");
                return (0, 0, 0);
            }

            var max = Mathf.Max(r, g, b);
            var min = Mathf.Min(r, g, b);
            var diff = max - min;

            var h = 0.0f;
            var s = 0.0f;
            var v = 0.0f;

            if (max != min)
            {
                // H(色相)  
                if (max == r)
                {
                    h = 60.0f * (g - b) / diff;
                }
                else if (max == g)
                {
                    h = 60.0f * (b - r) / diff + 120.0f;
                }
                else
                {
                    h = 60.0f * (r - g) / diff + 240.0f;
                }

                // S(彩度)
                s = diff / max;
            }

            if (h < 0)
            {
                h += 360.0f;
            }

            h = Mathf.Round(h);
            if (h >= 360.0f)
            {
                h -= 360.0f;
            }

            s = s * 100.0f;
            v = max * 100.0f;

            return ((int)h, (int)s, (int)v);
        }

        /// <summary>
        /// 2つの点から角度(0~360)を計算する
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public static float GetAngleFromVectors(Vector3 point1, Vector3 point2)
        {
            // 2つの点のベクトルを計算する
            var direction = point2 - point1;

            // atan2を使用してθを計算する（範囲は-πからπ）
            var thetaInRadians = Mathf.Atan2(direction.y, direction.x);

            // θの範囲を0から2πに変更する
            if (thetaInRadians < 0) {
                thetaInRadians += 2 * Mathf.PI;
            }

            // ラジアンを度に変換する
            return thetaInRadians * Mathf.Rad2Deg;
        }
    }
}