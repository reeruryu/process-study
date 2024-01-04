# process-study

<img src="https://img.shields.io/badge/Csharp-512BD4?style=for-the-badge&logo=csharp&logoColor=white">&nbsp;
<img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white">&nbsp;

## Architecture

<img src="https://github.com/reeruryu/process-study/assets/87798704/51948a74-0eef-468b-83fd-1b8d4f29adf0" alt="Alt Text" width="600"/>

## Description

<img src="https://github.com/reeruryu/process-study/assets/87798704/e233e080-221e-4e67-8729-ad5ff48cb9d8" alt="Alt Text" width="600"/><br>

📌 **Kill Process**: 종료

- 프로세스(pipe server)를 종료합니다.

---

📌 **Create Process**: 프로세스 생성

- 프로세스(pipe server)를 생성하고, 중복된 프로세스 생성을 방지하기 위해 mutex를 설정합니다.

---

📌 **Access Violation Exception**: 치명적인 예외

- 임의로 발생시키는 치명적인 예외로, 최대 2번까지 서버를 재기동합니다.

---

📌 **OK**: 정상 작동

- 서버가 정상적으로 작동하는 상태입니다.
