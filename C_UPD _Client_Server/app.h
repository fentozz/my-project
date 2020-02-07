#include <stdio.h>    
#include <stdlib.h>    
#include <string.h>
#include <limits.h>

#define ERROR_CREATE_THREAD -11
#define ERROR_JOIN_THREAD   -12
#define SUCCESS        0
#define ERROR          1

extern char* buff_console_str;
extern unsigned int size_buff_console;

char* Server_ip = "127.0.0.1";

#include <unistd.h>
#include <pthread.h>

int Start_new_thread(int status ,pthread_t* thread, void*(*foo)(void* args), void *args);


#include <sys/socket.h>
#include <sys/types.h>
#include <netinet/in.h>
#include <netdb.h>
#include <unistd.h>
#include <errno.h>
#include <arpa/inet.h>

int Create_socket(int p, struct sockaddr_in* serv);