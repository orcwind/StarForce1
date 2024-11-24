using UnityEngine;
using UnityGameFramework.Runtime;
using TMPro;

namespace StarForce
{
    public class DamageText : Entity
    {
        private TextMeshProUGUI m_TextMeshPro;
        private DamageTextData m_DamageTextData;
        private float m_Alpha = 1f;
        private Vector3 m_Velocity;
        private float m_LifeTime = 0f;
        
        // 配置参数
        private const float LIFE_DURATION = 2f;  // 持续时间
        private const float GRAVITY = -5f;  // 重力加速度
        private const float INITIAL_Y_SPEED_MIN = 2f;  // 最小初始向上速度
        private const float INITIAL_Y_SPEED_MAX = 3f;  // 最大初始向上速度
        private const float INITIAL_X_SPEED_RANGE = 2f;  // 左右随机速度范围
        private const float FADE_START_TIME = 1f;  // 开始淡出的时间点
        
        private const float CRITICAL_SCALE = 1.5f;
        
        private static readonly Color NORMAL_COLOR = Color.white;
        private static readonly Color CRITICAL_COLOR = Color.red;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            
            // 获取或添加TextMeshPro组件
            m_TextMeshPro = GetComponentInChildren<TextMeshProUGUI>();
            if (m_TextMeshPro != null)
            {
                // 重置子物体的本地坐标
                m_TextMeshPro.transform.localPosition = Vector3.zero;
                m_TextMeshPro.transform.localRotation = Quaternion.identity;
                m_TextMeshPro.transform.localScale = Vector3.one;
            }
            else
            {
                Log.Error("DamageText: Cannot find TextMeshProUGUI component");
            }
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            m_DamageTextData = userData as DamageTextData;
            if (m_DamageTextData == null)
            {
                Log.Error("DamageTextData is invalid.");
                return;
            }

            // 重置状态
            m_LifeTime = 0f;
            m_Alpha = 1f;
            
            // 设置初始位置，Y轴增加1个单位
            Vector3 adjustedPosition = m_DamageTextData.Position + Vector3.up;
            CachedTransform.position = adjustedPosition;
            
            // 设置初始速度
            float initialYSpeed = Random.Range(INITIAL_Y_SPEED_MIN, INITIAL_Y_SPEED_MAX);
            float initialXSpeed = Random.Range(-INITIAL_X_SPEED_RANGE, INITIAL_X_SPEED_RANGE);
            m_Velocity = new Vector3(initialXSpeed, initialYSpeed, 0);

            // 设置文本
            if (m_TextMeshPro != null)
            {
                // 重置文本位置
                m_TextMeshPro.transform.localPosition = Vector3.zero;
                m_TextMeshPro.transform.localRotation = Quaternion.identity;
                
                // 设置文本内容和样式
                m_TextMeshPro.text = Mathf.Round(m_DamageTextData.Damage).ToString();
                m_TextMeshPro.color = m_DamageTextData.IsCritical ? CRITICAL_COLOR : NORMAL_COLOR;
                m_TextMeshPro.transform.localScale = m_DamageTextData.IsCritical ? 
                    Vector3.one * CRITICAL_SCALE : Vector3.one;
            }
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            // 更新生命时间
            m_LifeTime += elapseSeconds;
            
            // 应用重力和移动
            m_Velocity.y += GRAVITY * elapseSeconds;
            CachedTransform.position += m_Velocity * elapseSeconds;

            // 处理淡出效果
            if (m_LifeTime >= FADE_START_TIME)
            {
                float fadeProgress = (m_LifeTime - FADE_START_TIME) / (LIFE_DURATION - FADE_START_TIME);
                m_Alpha = Mathf.Clamp01(1 - fadeProgress);
                
                if (m_TextMeshPro != null)
                {
                    Color color = m_TextMeshPro.color;
                    color.a = m_Alpha;
                    m_TextMeshPro.color = color;
                }
            }

            // 当完全透明时隐藏实体
            if (m_Alpha <= 0f)
            {
                GameEntry.Entity.HideEntity(this);
            }
        }
    }
}