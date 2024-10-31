# StarForce Game Project

## 目录
1. [项目概述](#项目概述)
2. [项目结构](#项目结构)
3. [核心模块](#核心模块)
4. [开发规范](#开发规范)
5. [依赖项](#依赖项)
6. [注意事项](#注意事项)
7. [常见问题](#常见问题)
8. [更新日志](#更新日志)

## 项目概述
StarForce 是一个 2D 像素风格的动作游戏项目，基于 Unity 引擎和 Game Framework 框架开发。

### 主要特性
- 流畅的角色控制系统
- 丰富的武器系统
- 交互式物品系统
- 可扩展的实体系统

## 项目结构
```
Assets/
├── GameMain/
│   ├── Scripts/
│   │   ├── Base/                 # 基础框架代码
│   │   ├── Entity/               # 实体系统
│   │   │   ├── EntityData/      # 实体数据类
│   │   │   └── EntityLogic/     # 实体逻辑类
│   │   │       ├── Control/     # 控制器相关
│   │   │       └── Inventory/   # 物品系统相关
│   │   ├── Game/                # 游戏核心逻辑
│   │   ├── Definition/          # 常量和枚举定义
│   │   └── Utility/            # 工具类
│   ├── Entities/                # 实体预制件
│   ├── UI/                      # UI预制件
│   └── DataTables/             # 数据表
└── README.md
```

## 核心模块

### 1. 实体系统 (Entity System)
#### 基础类
- `Entity.cs`: 所有实体的基类
- `EntityData.cs`: 实体数据基类

#### 主要实体
- `Character.cs`: 角色基类
  - 处理基础角色逻辑
  - 管理角色状态和属性
- `Player.cs`: 玩家实体
  - 继承自 Character
  - 处理玩家特有逻辑
- `GroundWeapon.cs`: 地面武器实体
  - 实现 IInteractable 接口
  - 处理武器拾取和丢弃逻辑

### 2. 控制系统 (Control System)
#### 玩家控制
- `PlayerController.cs`: 处理玩家输入
  ```csharp
  public class PlayerController : MonoBehaviour
  {
      // 处理移动输入
      private void HandleMovement();
      // 处理攻击输入
      private void HandleAttack();
      // 处理交互输入
      private void HandleInteraction();
  }
  ```

#### 角色移动
- `CharacterMotor.cs`: 处理角色移动逻辑
  ```csharp
  public class CharacterMotor : MonoBehaviour
  {
      // 基础移动
      public void Move(Vector2 direction);
      // 跳跃
      public void Jump();
      // 翻滚
      public void Roll();
  }
  ```

### 3. 物品系统 (Inventory System)
#### 武器管理
- `WeaponManager.cs`: 单例模式，管理所有武器
  ```csharp
  public class WeaponManager : MonoBehaviour
  {
      // 生成地面武器
      public void SpawnWeaponOnGround(int weaponId, Vector3 position);
      // 注册地面武器
      public void RegisterGroundWeapon(GroundWeapon weapon);
  }
  ```

#### 数据结构
- `WeaponData.cs`: 武器数据类
- `WeaponInfo.cs`: 武器信息类

## 开发规范

### 1. 命名规范
- **类名**: PascalCase
  ```csharp
  public class PlayerController
  ```
- **接口**: I前缀 + PascalCase
  ```csharp
  public interface IInteractable
  ```
- **私有字段**: m_前缀 + PascalCase
  ```csharp
  private PlayerData m_PlayerData;
  ```
- **公共属性**: PascalCase
  ```csharp
  public int WeaponId { get; set; }
  ```

### 2. 注释规范
```

### 3. 代码组织
- 使用 `StarForce` 作为根命名空间
- 按功能模块组织文件夹
- 相关功能放在同一命名空间下

## 依赖项
- Unity 2020.3+
- Game Framework
- Input System Package
- 2D Package

## 注意事项
1. 实体生命周期管理
   - 使用 GameEntry.Entity 创建和销毁实体
   - 正确实现 OnInit/OnShow/OnHide 方法
   - 注意资源释放

2. 数据管理
   - 所有数据类需要实现序列化
   - 使用数据表管理配置数据
   - 注意数据的同步和更新

3. 性能优化
   - 使用对象池
   - 避免频繁的 GC
   - 优化物理检测

## 常见问题

### 1. 实体显示问题
- 检查实体预制件是否正确配置
- 确认实体数据是否正确
- 查看是否正确订阅实体事件

### 2. 武器系统问题
- 武器切换逻辑说明
- 武器数据配置指南
- 常见问题排查步骤

### 3. 交互系统问题
- 交互检测范围设置
- 交互优先级处理
- UI提示显示问题

## 更新日志

### Version 0.1.0 (2024-03-xx)
- 实现基础角色控制系统
- 添加武器系统
- 实现交互系统

### Version 0.0.1 (2024-03-xx)
- 项目初始化
- 基础框架搭建
- 实体系统实现

## 贡献指南
1. Fork 项目
2. 创建特性分支
3. 提交更改
4. 推送到分支
5. 创建 Pull Request

## 联系方式
- 项目维护者：[维护者姓名]
- 邮箱：[联系邮箱]