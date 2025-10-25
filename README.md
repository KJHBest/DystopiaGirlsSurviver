# Dystopia Girls Surviver

뱀파이어 서바이버 장르의 Unity 게임 프로젝트

## 프로젝트 구조

```
Assets/
├── Scenes/              # Unity 씬 파일
├── Scripts/             # C# 스크립트
│   ├── Managers/        # 게임 매니저
│   ├── Player/          # 플레이어 관련
│   ├── Enemy/           # 적 AI 및 스폰
│   ├── UI/              # UI 시스템
│   ├── Weapon/          # 무기 시스템
│   ├── Systems/         # 게임 시스템
│   └── Data/            # 데이터 클래스/ScriptableObjects
├── Prefabs/             # 프리팹
└── Resources/           # 리소스 파일
```

## 주요 시스템

### 1. 게임 매니저 (GameManager)
- 게임 상태 관리 (일시정지, 게임오버 등)
- 씬 전환 관리
- 싱글톤 패턴 구현

**위치**: `Assets/Scripts/Managers/GameManager.cs`

### 2. 메인 메뉴 (MainMenuUI)
- 게임 시작
- 메타 업그레이드 메뉴
- 게임 설정
- 게임 종료

**위치**: `Assets/Scripts/UI/MainMenuUI.cs`

### 3. 플레이어 시스템

#### PlayerController
- WASD/방향키 이동
- Rigidbody2D 기반 물리 이동

**위치**: `Assets/Scripts/Player/PlayerController.cs`

#### PlayerStats
- 체력 관리
- 데미지 처리
- 플레이어 사망 처리

**위치**: `Assets/Scripts/Player/PlayerStats.cs`

### 4. 적 시스템

#### Enemy
- 플레이어 추적 AI
- 충돌 시 데미지
- 사망 시 경험치 드롭

**위치**: `Assets/Scripts/Enemy/Enemy.cs`

#### EnemySpawner
- 화면 밖에서 적 스폰
- 스폰 간격 및 최대 수 관리

**위치**: `Assets/Scripts/Enemy/EnemySpawner.cs`

### 5. 무기 시스템

#### WeaponBase (추상 클래스)
- 모든 무기의 베이스 클래스
- 데미지, 공격속도 등 기본 스탯

**위치**: `Assets/Scripts/Weapon/WeaponBase.cs`

#### AutoWeapon
- 자동으로 가장 가까운 적 공격
- 발사체 생성 및 발사

**위치**: `Assets/Scripts/Weapon/AutoWeapon.cs`

#### Projectile
- 발사체 동작
- 적 충돌 시 데미지

**위치**: `Assets/Scripts/Weapon/Projectile.cs`

### 6. 경험치 및 레벨업 시스템 (ExperienceSystem)
- 경험치 획득 및 관리
- 레벨업 처리
- 레벨업 시 자동으로 게임 일시정지

**위치**: `Assets/Scripts/Systems/ExperienceSystem.cs`

### 7. 스킬 업그레이드 시스템

#### SkillUpgradeData
- ScriptableObject 기반 스킬 데이터
- 다양한 업그레이드 타입 지원

**위치**: `Assets/Scripts/Data/SkillUpgradeData.cs`

#### SkillUpgradeUI
- 레벨업 시 3개 카드 표시
- 스킬 선택 및 적용

**위치**: `Assets/Scripts/UI/SkillUpgradeUI.cs`

### 8. 게임 HUD (GameHUD)
- 좌상단: 체력바, 보유 스킬
- 상단: 경험치바, 레벨 표시

**위치**: `Assets/Scripts/UI/GameHUD.cs`

## 게임 플레이 흐름

1. **메인 메뉴**
   - 게임 시작 버튼 클릭
   - 메타 업그레이드 메뉴 (추후 구현)
   - 게임 설정 (추후 구현)
   - 게임 종료

2. **게임 플레이**
   - 플레이어 WASD로 이동
   - 무기가 자동으로 적 공격
   - 적이 화면 밖에서 스폰되어 플레이어 추격
   - 적 처치 시 경험치 획득

3. **레벨업**
   - 경험치가 가득 차면 레벨업
   - 게임 일시정지
   - 3개의 스킬 카드 중 1개 선택
   - 선택 후 게임 재개

4. **게임 오버**
   - 체력이 0이 되면 게임 오버
   - 게임 오버 UI 표시 (추후 구현)

## 필요한 Unity 설정

### 태그 (Tags)
- `Player`: 플레이어 오브젝트에 설정
- `Enemy`: 적 오브젝트에 설정

### 레이어 (Layers)
- 적 감지를 위한 레이어 설정 필요

### Input Manager
- Horizontal: A/D 또는 Left/Right
- Vertical: W/S 또는 Up/Down

## 다음 구현 예정 기능

- [ ] 메타 업그레이드 시스템
- [ ] 게임 설정 (사운드, 그래픽 등)
- [ ] 다양한 무기 타입
- [ ] 다양한 적 타입
- [ ] 파워업 아이템
- [ ] 보스 적
- [ ] 스테이지 시스템
- [ ] 세이브/로드 시스템

## 기술 스택

- Unity 2021.3 LTS 이상 권장
- C# 8.0+
- TextMeshPro (UI 텍스트)
- Unity UI System

## 개발 시 주의사항

1. **플레이어 설정**
   - Player 태그 필수
   - Rigidbody2D 컴포넌트 필요
   - Collider2D 컴포넌트 필요

2. **적 설정**
   - Enemy 태그 필수
   - Rigidbody2D 컴포넌트 필요 (Gravity Scale = 0)
   - Collider2D 컴포넌트 필요

3. **무기 설정**
   - 플레이어의 자식 오브젝트로 배치
   - Projectile 프리팹 필요

4. **UI 설정**
   - Canvas 스케일러 설정
   - EventSystem 필요
