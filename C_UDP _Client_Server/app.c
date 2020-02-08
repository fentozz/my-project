
char* buff_console_str;//буфер считывания из консоли
unsigned int size_buff_console = 80; //размер буфера 

struct Segment   {
   unsigned long long start : 64;
   unsigned long long step : 64;
   unsigned long long curr : 64;
};

typedef struct {
   int Seg;
}Arg_th;

int Start_new_thread(int status ,pthread_t* thread, void*(*foo)(void* args), void* args)
{
   status = pthread_create(&*thread, NULL, foo, args);
   if (status != 0) {
      printf(" error: can't create thread console, status = %d\n", status);
      exit(ERROR_CREATE_THREAD);
   }
   return status;
}

int Create_socket(int  port, struct sockaddr_in* serv){
   bzero(&*serv, sizeof(serv));
   serv->sin_family = AF_INET;
   serv->sin_port = htons(port);
   inet_pton(AF_INET, Server_ip, &serv->sin_addr);
   return socket(AF_INET, SOCK_DGRAM, 0);
}