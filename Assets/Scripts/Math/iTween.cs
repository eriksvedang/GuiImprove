using UnityEngine;
using System;
using System.Collections.Generic;
public class iTween
{
    public static readonly iTween.MultiDimTween<Vector2> vector2Bounce = new iTween.MultiDimTween<Vector2>(Vector2.Lerp, iTween.bounce);
    public static readonly iTween.MultiDimTween<Vector2> vector2easeInOutSine = new iTween.MultiDimTween<Vector2>(Vector2.Lerp, iTween.easeInOutSine);
    public static readonly iTween.MultiDimTween<Vector2> vector2easeInOutExpo = new iTween.MultiDimTween<Vector2>(Vector2.Lerp, iTween.easeInOutExpo);
    public static readonly iTween.MultiDimTween<Vector2> vector2spring = new iTween.MultiDimTween<Vector2>(Vector2.Lerp, iTween.spring);

    public class MultiDimTween<T>
    {
        public delegate float OneDimLerp(float pStart, float pEnd, float pValue );
        public delegate T InterpolationSampler(T pStart, T pEnd, float pValue);
        InterpolationSampler a;
        OneDimLerp b;
        public MultiDimTween( InterpolationSampler pMultiDimSampler, OneDimLerp pSingleTimensionLerp )
        {
            a = pMultiDimSampler;
            b = pSingleTimensionLerp;
        }
        public T Sample(T pStart, T pEnd, float pValue)
        {
            return a(pStart, pEnd, b(0, 1, pValue));
        }
    }

    
	static public float linear(float start, float end, float value)
    {
		return Mathf.Lerp(start, end, value);
	}
    static public float clerp(float start, float end, float value)
    {
		float min = 0.0f;
		float max = 360.0f;
		float half = Mathf.Abs((max - min) / 2.0f);
		float retval = 0.0f;
		float diff = 0.0f;
		if ((end - start) < -half){
			diff = ((max - start) + end) * value;
			retval = start + diff;
		}else if ((end - start) > half){
			diff = -((max - end) + start) * value;
			retval = start + diff;
		}else retval = start + (end - start) * value;
		return retval;
    }

    static public float spring(float start, float end, float value)
    {
		value = Mathf.Clamp01(value);
		value = (Mathf.Sin(value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + (1.2f * (1f - value)));
		return start + (end - start) * value;
	}

    static public float easeInQuad(float start, float end, float value)
    {
		end -= start;
		return end * value * value + start;
	}

    static public float easeOutQuad(float start, float end, float value)
    {
		end -= start;
		return -end * value * (value - 2) + start;
	}

    static public float easeInOutQuad(float start, float end, float value)
    {
		value /= .5f;
		end -= start;
		if (value < 1) return end / 2 * value * value + start;
		value--;
		return -end / 2 * (value * (value - 2) - 1) + start;
	}

    static public float easeInCubic(float start, float end, float value)
    {
		end -= start;
		return end * value * value * value + start;
	}

    static public float easeOutCubic(float start, float end, float value)
    {
		value--;
		end -= start;
		return end * (value * value * value + 1) + start;
	}

    static public float easeInOutCubic(float start, float end, float value)
    {
		value /= .5f;
		end -= start;
		if (value < 1) return end / 2 * value * value * value + start;
		value -= 2;
		return end / 2 * (value * value * value + 2) + start;
	}

    static public float easeInQuart(float start, float end, float value)
    {
		end -= start;
		return end * value * value * value * value + start;
	}

    static public float easeOutQuart(float start, float end, float value)
    {
		value--;
		end -= start;
		return -end * (value * value * value * value - 1) + start;
	}

    static public float easeInOutQuart(float start, float end, float value)
    {
		value /= .5f;
		end -= start;
		if (value < 1) return end / 2 * value * value * value * value + start;
		value -= 2;
		return -end / 2 * (value * value * value * value - 2) + start;
	}

    static public float easeInQuint(float start, float end, float value)
    {
		end -= start;
		return end * value * value * value * value * value + start;
	}

    static public float easeOutQuint(float start, float end, float value)
    {
		value--;
		end -= start;
		return end * (value * value * value * value * value + 1) + start;
	}

    static public float easeInOutQuint(float start, float end, float value)
    {
		value /= .5f;
		end -= start;
		if (value < 1) return end / 2 * value * value * value * value * value + start;
		value -= 2;
		return end / 2 * (value * value * value * value * value + 2) + start;
	}

    static public float easeInSine(float start, float end, float value)
    {
		end -= start;
		return -end * Mathf.Cos(value / 1 * (Mathf.PI / 2)) + end + start;
	}

    static public float easeOutSine(float start, float end, float value)
    {
		end -= start;
		return end * Mathf.Sin(value / 1 * (Mathf.PI / 2)) + start;
	}

    static public float easeInOutSine(float start, float end, float value)
    {
		end -= start;
		return -end / 2 * (Mathf.Cos(Mathf.PI * value / 1) - 1) + start;
	}

    static public float easeInExpo(float start, float end, float value)
    {
		end -= start;
		return end * Mathf.Pow(2, 10 * (value / 1 - 1)) + start;
	}

    static public float easeOutExpo(float start, float end, float value)
    {
		end -= start;
		return end * (-Mathf.Pow(2, -10 * value / 1) + 1) + start;
	}

    static public float easeInOutExpo(float start, float end, float value)
    {
		value /= .5f;
		end -= start;
		if (value < 1) return end / 2 * Mathf.Pow(2, 10 * (value - 1)) + start;
		value--;
		return end / 2 * (-Mathf.Pow(2, -10 * value) + 2) + start;
	}

    static public float easeInCirc(float start, float end, float value)
    {
		end -= start;
		return -end * (Mathf.Sqrt(1 - value * value) - 1) + start;
	}

    static public float easeOutCirc(float start, float end, float value)
    {
		value--;
		end -= start;
		return end * Mathf.Sqrt(1 - value * value) + start;
	}

    static public float easeInOutCirc(float start, float end, float value)
    {
		value /= .5f;
		end -= start;
		if (value < 1) return -end / 2 * (Mathf.Sqrt(1 - value * value) - 1) + start;
		value -= 2;
		return end / 2 * (Mathf.Sqrt(1 - value * value) + 1) + start;
	}

    static public float bounce(float start, float end, float value)
    {
		value /= 1f;
		end -= start;
		if (value < (1 / 2.75f)){
			return end * (7.5625f * value * value) + start;
		}else if (value < (2 / 2.75f)){
			value -= (1.5f / 2.75f);
			return end * (7.5625f * (value) * value + .75f) + start;
		}else if (value < (2.5 / 2.75)){
			value -= (2.25f / 2.75f);
			return end * (7.5625f * (value) * value + .9375f) + start;
		}else{
			value -= (2.625f / 2.75f);
			return end * (7.5625f * (value) * value + .984375f) + start;
		}
	}

    static public float easeInBack(float start, float end, float value)
    {
		end -= start;
		value /= 1;
		float s = 1.70158f;
		return end * (value) * value * ((s + 1) * value - s) + start;
	}

    static public float easeOutBack(float start, float end, float value)
    {
		float s = 1.70158f;
		end -= start;
		value = (value / 1) - 1;
		return end * ((value) * value * ((s + 1) * value + s) + 1) + start;
	}

    static public float easeInOutBack(float start, float end, float value)
    {
		float s = 1.70158f;
		end -= start;
		value /= .5f;
		if ((value) < 1){
			s *= (1.525f);
			return end / 2 * (value * value * (((s) + 1) * value - s)) + start;
		}
		value -= 2;
		s *= (1.525f);
		return end / 2 * ((value) * value * (((s) + 1) * value + s) + 2) + start;
	}

    static public float punch(float amplitude, float value)
    {
		float s = 9;
		if (value == 0){
			return 0;
		}
		if (value == 1){
			return 0;
		}
		float period = 1 * 0.3f;
		s = period / (2 * Mathf.PI) * Mathf.Asin(0);
		return (amplitude * Mathf.Pow(2, -10 * value) * Mathf.Sin((value * 1 - s) * (2 * Mathf.PI) / period));
    }

    static public float elastic(float start, float end, float value)
    {
		//Thank you to rafael.marteleto for fixing this as a port over from Pedro's UnityTween
		end -= start;
		
		float d = 1f;
		float p = d * .3f;
		float s = 0;
		float a = 0;
		
		if (value == 0) return start;
		
		if ((value /= d) == 1) return start + end;
		
		if (a == 0f || a < Mathf.Abs(end)){
			a = end;
			s = p / 4;
			}else{
			s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
		}
		
		return (a * Mathf.Pow(2, -10 * value) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p) + end + start);
	}
} 
