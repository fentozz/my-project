#include "app.h"
#include "app.c"

#define SEGMENT_ONE 1
#define SEGMENT_TWO 2
#define SEGMENT_THREE 3

unsigned long long Curr_sequence[] ={0,0,0};
char* sep = " ";

int Set_segment(int num_segment, char* str)
{
    struct sockaddr_in servaddr;
    int sockfd = Create_socket(5000 + num_segment, &servaddr);

    unsigned long long *send_buf =malloc(sizeof(unsigned long long)); 
    send_buf[0] = 0;

    char* sepch;
    sepch = strtok(buff_console_str, sep);
    unsigned long long segment[2];
    
    int count = 0;
    if (sepch!= NULL){
        for (int i =0 ; i < 3; i++){
            //printf("%s\n",sepch);
            if (sepch!= NULL){
                count++;
                if (i != 0) {
                    sscanf(sepch,"%llu",&segment[i-1]); 
                    if (segment[i-1] == 0) return 1;
                }
                               
                sepch = strtok(NULL, sep);
            }                        
        }
        if (sepch != NULL) count++;            
    }
    if (count !=3) return 1;

    for (int i = 0; i < 2; i++){
       send_buf = &segment[i];
       //printf("send %llu\n",send_buf[0]);
       sendto(sockfd, send_buf, sizeof(send_buf), 0, (struct sockaddr*)&servaddr, sizeof(servaddr));
    }

    close(sockfd);
    return 0;             
}

void* Inp_Segment(void *args) {  
    Arg_th *arg = (Arg_th*) args;
    struct sockaddr_in servaddr;
    int sockfd = Create_socket(5003+arg->Seg, &servaddr);

    unsigned long long *buff =malloc(sizeof(unsigned long long)); 
    buff[0] = ULLONG_MAX;

    int byte = 0;
    byte = sendto(sockfd, buff, sizeof(buff), 0, (struct sockaddr*)&servaddr, sizeof(servaddr));

    while(1){
        recvfrom(sockfd, buff, sizeof(buff), 0, NULL, NULL);
        Curr_sequence[arg->Seg-1] = buff[0];
    }

    close(sockfd);
    return SUCCESS;
}


void* Console(void* args){
    buff_console_str = (char*)malloc(size_buff_console * sizeof(char));

    struct sockaddr_in serv_addr;
    
    while ( printf(">") ) { //вежливо приглашаем 
        fgets(buff_console_str,80,stdin);
        buff_console_str[strlen(buff_console_str)-1] = '\0';

        if (strcmp(buff_console_str, "Exit") == 0)//выход
            break;
        else if(strcmp(buff_console_str, "Status") == 0){
            printf("Current sequence - %llu %llu %llu \n", Curr_sequence[0], Curr_sequence[1], Curr_sequence[2]);
        }
        else if(strcmp(buff_console_str, "Status constantly") == 0){
            while(1){
                printf("Current sequence - %llu %llu %llu \n", Curr_sequence[0], Curr_sequence[1], Curr_sequence[2]);
                sleep(2);
            }
        }
        else if (strncmp(buff_console_str, "Seg1", 4) == 0) {// 1 сегмент 
            if ( Set_segment(SEGMENT_ONE,buff_console_str) !=0)
            printf("Incorrect command\n");
        }
        else if (strncmp(buff_console_str, "Seg2", 4) == 0) {// 2 сегмент
            if ( Set_segment(SEGMENT_TWO,buff_console_str) !=0)
            printf("Incorrect command\n");
        }
        else if (strncmp(buff_console_str, "Seg3", 4) == 0) {// 3 сегмент
           if ( Set_segment(SEGMENT_THREE,buff_console_str) !=0)
            printf("Incorrect command\n");
        }
        else printf("Invalid command\n");

        buff_console_str = (char*)malloc(size_buff_console * sizeof(char));
    }
}

int main(void)               
{  
    pthread_t thread_Console, thread_outSegment1, thread_outSegment2, thread_outSegment3;
    int status_Console, status_inpSegment;
    int status_addr_Console, status_addr_outSegment;
    
    printf("Hello, i'm client\n");  
    printf("************************************************\n");
    printf("*Status - output current sequence              *\n");
    printf("*Status constantly - output current sequence   *\n");
    printf("*Seg1 xxxx yyyy - set 1 segment                *\n");
    printf("*Seg2 xxxx yyyy - set 2 segment                *\n");
    printf("*Seg3 xxxx yyyy - set 3 segment                *\n");
    printf("*Exit - go out                                 *\n");
    printf("************************************************\n");  

    Arg_th seg[3] = {1,2,3};

    Start_new_thread(status_Console, &thread_Console,Console, NULL);
    Start_new_thread(status_inpSegment, &thread_outSegment1, Inp_Segment, (void*)&seg[0]);
    pthread_detach(thread_outSegment1);
    Start_new_thread(status_inpSegment, &thread_outSegment2, Inp_Segment, (void*)&seg[1]);
    pthread_detach(thread_outSegment2);
    Start_new_thread(status_inpSegment, &thread_outSegment3, Inp_Segment, (void*)&seg[2]);
    pthread_detach(thread_outSegment3);

    status_Console = pthread_join(thread_Console, (void**)&status_addr_Console);
    if (status_Console != SUCCESS) {
        printf("error: can't join thread, status = %d\n", status_Console);
        exit(ERROR_JOIN_THREAD);
    }

    pthread_cancel(thread_outSegment1);
    pthread_cancel(thread_outSegment2);
    pthread_cancel(thread_outSegment3);
 
    //printf("joined with address %d\n", status_addr_Console);                    
   
    printf("Bye\n");
    return 0;    
}