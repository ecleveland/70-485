# Threads and ThreadPool

Threads are a lower level of abstraction. Tasks are an item of work whereas Threads represent a process running on an operating system.

## Important Differences

### Threads vs Tasks

- Threads are created as *foreground* processes
  - Processes will not terminate when a foreground thread is active
  - Tasks are *background* processes that will be cancelled if a foreground thread is completed
- Threads have a *priority* property changeable during lifetime of the thread. It is intended to allocate a portion of processor time
- Threads cant deliver to new threads or other threads
  - They talk using shared variables and this can cause sync issues
- There are no continuations just joins which allows one thread to pause until another completes
- It is not possible to aggregate exceptions over a number of threads. The thread catches and deals with exceptions in the thread code. Tasks allow aggregate exceptions

Other notes included in the program code.
