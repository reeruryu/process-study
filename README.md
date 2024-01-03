# process-study

## form
<img src="https://github.com/reeruryu/process-study/assets/87798704/e233e080-221e-4e67-8729-ad5ff48cb9d8" alt="Alt Text" width="600"/>



1. kill process: 프로세스(pipe server) 종료
2. create process: 프로세스(pipe server) 생성, mutex 설정(중복된 프로세스 생성 방지)
3. access violation exception: 치명적인 exception (임의로 throw하는 중. 최대 2번 서버 재기동)
4. ok: 정상 작동

## architecture
<img src="https://github.com/reeruryu/process-study/assets/87798704/51948a74-0eef-468b-83fd-1b8d4f29adf0" alt="Alt Text" width="600"/>



