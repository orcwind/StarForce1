//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.DataTable;
using System;
using UnityGameFramework.Runtime;
using UnityEngine;
using GameFramework;
using GameFramework.Entity;

namespace StarForce
{
    public static class EntityExtension
    {
        // 关于 EntityId 的约定：
        // 0 为无效
        // 正值用于和服务器通信的实体（如玩家角色、NPC、怪等，服务器只产生正值）
        // 负值用于本地生成的临时实体（如特效、FakeObject等）
        private static int s_SerialId = 0;

        public static Entity GetGameEntity(this EntityComponent entityComponent, int entityId)
        {
            UnityGameFramework.Runtime.Entity entity = entityComponent.GetEntity(entityId);
            if (entity == null)
            {
                return null;
            }

            return (Entity)entity.Logic;
        }

        public static void HideEntity(this EntityComponent entityComponent, Entity entity)
        {
            entityComponent.HideEntity(entity.Entity);
        }

        public static void AttachEntity(this EntityComponent entityComponent, Entity entity, int ownerId, string parentTransformPath = null, object userData = null)
        {
            entityComponent.AttachEntity(entity.Entity, ownerId, parentTransformPath, userData);
        }

        public static void ShowPlayer(this EntityComponent entityComponent, PlayerData data)
        {
            entityComponent.ShowEntity(typeof(Player), "Player", Constant.AssetPriority.PlayerAsset, data);
        }

        public static void ShowEffect(this EntityComponent entityComponent, EffectData data)
        {
            entityComponent.ShowEntity(typeof(Effect), "Effect", Constant.AssetPriority.EffectAsset, data);
        }

        public static void ShowBackGround(this EntityComponent entityComponent, BackgroundData data)
        {
            entityComponent.ShowEntity(typeof(Background), "Background", Constant.AssetPriority.BackgroundAsset, data);
        }

        public static void ShowAttack(this EntityComponent entityComponent, AttackData data)
        {
            if (data == null)
            {
                Log.Error("AttackData is null in ShowAttack");
                return;
            }

            try 
            {
                // 获取实体配置
                IDataTable<DREntity> dtEntity = GameEntry.DataTable.GetDataTable<DREntity>();
                DREntity drEntity = dtEntity.GetDataRow(data.TypeId);
                if (drEntity == null)
                {
                    Log.Warning("Can not load entity id '{0}' from data table.", data.TypeId.ToString());
                    return;
                }

                // 使用完整的 ShowEntity 方法
                entityComponent.ShowEntity(
                    data.Id,                         // 实体ID
                    typeof(Attack),                  // 实体类型
                    AssetUtility.GetEntityAsset(drEntity.AssetName),  // 资源名称
                    "Attack",                        // 实体组名称
                    Constant.AssetPriority.AttackAsset,  // 优先级
                    data                            // 用户数据
                );

                //Log.Info($"Showing Attack entity: Id={data.Id}, TypeId={data.TypeId}, AssetName={drEntity.AssetName}");
            }
            catch (Exception e)
            {
                Log.Error($"Error in ShowAttack: {e.Message}\nStackTrace: {e.StackTrace}");
            }
        }

        public static void ShowGroundWeapon(this EntityComponent entityComponent, GroundWeaponData data)
        {
            entityComponent.ShowEntity(typeof(GroundWeapon), "GroundWeapon", Constant.AssetPriority.GroundWeaponAsset, data);
        }

        public static void ShowEnemy(this EntityComponent entityComponent, EnemyData data)
        {
            entityComponent.ShowEntity(typeof(Enemy), "Enemy", Constant.AssetPriority.EnemyAsset, data);
        }

public static void ShowDamageText(this EntityComponent entityComponent, DamageTextData data)
{
            entityComponent.ShowEntity(typeof(DamageText), "DamageText", Constant.AssetPriority.DamageTextAsset, data);
            Log.Info("Showing DamageText entity: Id={data.Id}, TypeId={data.TypeId}, AssetName={drEntity.AssetName}");
        }
        private static void ShowEntity(this EntityComponent entityComponent, Type logicType, string entityGroup, int priority, EntityData data)
        {
            if (data == null)
            {
                Log.Warning("Data is invalid.");
                return;
            }

            IDataTable<DREntity> dtEntity = GameEntry.DataTable.GetDataTable<DREntity>();
            DREntity drEntity = dtEntity.GetDataRow(data.TypeId);
            if (drEntity == null)
            {
                Log.Warning("Can not load entity id '{0}' from data table.", data.TypeId.ToString());
                return;
            }

            entityComponent.ShowEntity(data.Id, logicType, AssetUtility.GetEntityAsset(drEntity.AssetName), entityGroup, priority, data);
        }

        public static int GenerateSerialId(this EntityComponent entityComponent)
        {
            return --s_SerialId;
        }
    }
}
