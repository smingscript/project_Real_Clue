# Real Clue(Project Real_Clue)
이 프로젝트는 클루라는 보드게임을 3D 온라인 게임으로 개발합니다.
기존의 턴 기반 추리 시스템을 유지하면서, 사용자가 플레이하기 편한 네트워크를 이용한 자동화된 UI를 통해 보드게임의 한계를 개선합니다.

## 시작하기
> 이 리포지토리에는 용량의 문제로 프로젝트의 스크립트만 배포하고 있습니다. 지금까지 진행된 프로젝트의 전체는 구글 드라이브( https://goo.gl/ojP6Uq )에서 다운받을 수 있습니다.
> 프로젝트에 사용된 일부 유료 리소스의 저작권의 문제로 3D 캐릭터 prefab은 Capsule로 대체되어 있습니다.

### 프로그램 데모 실행하기
> 1. 구글 드라이브 링크( https://goo.gl/ojP6Uq )로 접속하여 이 프로젝트의 소스를 다운받습니다. Unity 3D에서 프로젝트를 엽니다.
> 2. https://www.photonengine.com/ 에 로그인하여 대시보드의 어플리케이션 ID을 복사합니다.
> 3. `/Real Clue/Assets/Photon Unity Networking/Resources` 폴더의 PhotonServerSetting의 AppId에 복사한 어플리케이션 ID를 등록합니다.
> 4. Test Run을 할 때 `/Real Clue/Assets/Scene/LobbyScenes`에 있는 Launcher 씬에서 실행해야 네트워크에 접속할 수 있습니다.
> 5. Unity 메뉴의 File-Build Settings에서 프로젝트를 빌드합니다.
> 6. 빌드된 [빌드한_파일명].exe를 실행합니다.(본 게임은 3인 이상으로 실행해야 카드 UI가 맞게 실행됩니다.)

## 개발환경
- Unity3D 버전: 2017.4.1.f1
- Unity3D Scripting Runtime Version: Experimental(.NET Framework 4.6)
- IDE: Visual Studio 2017

## 라이선스
This project is licensed under the MIT License
