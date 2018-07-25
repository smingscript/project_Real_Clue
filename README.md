# Real Clue(Project Real_Clue)
이 프로젝트는 클루라는 보드게임을 3D 온라인 게임으로 개발합니다.
기존의 턴 기반 추리 시스템을 유지하면서, 사용자가 플레이하기 편한 네트워크를 이용한 자동화된 UI를 통해 보드게임의 한계를 개선합니다.

## 시작하기
### 프로그램 데모 실행하기
> 프로젝트에 사용된 일부 유료 리소스의 저작권의 문제로 3D 캐릭터 prefab은 깃헙에 포함하지 않았습니다.
> 1. 이 레포지토리를 다운받고, Unity 3D에서 프로젝트를 엽니다.
> 2. 구글 드라이브 링크(https://goo.gl/ojP6Uq)로 접속하여 Board.dae를 `/Real_Clue/Real Clue/Assets/House` 폴더에 저장합니다. Unity에서 `Scene/GameScenes/Main`을 열어 `REAL CLUE` 게임 오브젝트 선택하고 저장한 Board
> 3. `/Real_Clue/Real Clue/Assets/Photon Unity Networking/Resources` 폴더의 PhotonServerSetting의 AppId에 Photon Engine 계정에서 발급받은 AppId를 등록합니다.
> 4. Test Run을 할 때 `/Real_Clue/Real Clue/Assets/Scene/LobbyScenes`에 있는 Launcher 씬에서 실행해야 네트워크에 접속할 수 있습니다.
> 5. Unity 메뉴의 File-Build Settings에서 프로젝트를 빌드합니다.
> 6. 빌드된 [빌드한_파일명].exe를 실행합니다.(본 게임은 3인 이상으로 실행해야 카드 UI가 맞게 실행됩니다.)

## 개발환경
- Unity3D 버전: 2017.4.1.f1
- Unity3D Scripting Runtime Version: Experimental(.NET Framework 4.6)
- IDE: Visual Studio 2017

## 라이선스
This project is licensed under the MIT License
