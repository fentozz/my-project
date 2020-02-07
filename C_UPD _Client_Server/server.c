#include "app.h"
#include "app.c"

pthread_mutex_t mutex;

struct Segment Current_segment[3]={0,0,0,0,0,0,0,0,0};

void* Calculate_sequence(void *args){
    while (1)
    {
        for(int i = 0; i <3; i++){
            pthread_mutex_lock(&mutex);
            if ((Current_segment[i].curr+ Current_segment[i].step ) <ULLONG_MAX)
            Current_segment[i].curr = Current_segment[i].curr + Current_segment[i].step;
            else Current_segment[i].curr = Current_segment[i].start;
            
            printf("%llu ",Current_segment[i].curr );
            pthread_mutex_unlock(&mutex);
        }
        printf("\n");
        sleep(2);
    }
}

void* Input_segment(void *args) {
    Arg_th *arg = (Arg_th*) args;

    struct sockaddr_in servaddr, cliaddr;
    int sockfd = Create_socket(5000+arg->Seg, &servaddr);
    servaddr.sin_addr.s_addr = htonl(INADDR_ANY);//в локальную петлю
    bind(sockfd, (struct sockaddr*)&servaddr, sizeof(servaddr));

    socklen_t len;

    unsigned long long *rec_buf =malloc(sizeof(unsigned long long)); 
    rec_buf[0] = ULLONG_MAX;
    unsigned long long start, step;

    while(1){
        len = sizeof(cliaddr);

        recvfrom(sockfd, rec_buf, sizeof(rec_buf), 0, (struct sockaddr*)&cliaddr, &len);
        start = rec_buf[0];
        recvfrom(sockfd, rec_buf, sizeof(rec_buf), 0, (struct sockaddr*)&cliaddr, &len);
        step = rec_buf[0];
        printf("New segment%d - start: %llu step: %llu\n", arg->Seg ,start,step);
        pthread_mutex_lock(&mutex);
        Current_segment[arg->Seg -1].start = start;
        Current_segment[arg->Seg -1].step = step;
        Current_segment[arg->Seg -1].curr = start;
        pthread_mutex_unlock(&mutex);
        sleep(1);
    }    
    return SUCCESS;
}

struct sockaddr_in connecting1[10];
int c_cl1=0;
struct sockaddr_in connecting2[10];
int c_cl2=0;
struct sockaddr_in connecting3[10];
int c_cl3=0;

int soc_connect1;
int soc_connect2;
int soc_connect3;
struct sockaddr_in serv_connect[3];


void add_connected(struct sockaddr_in new_conn , int seg){
    struct sockaddr_in con_m;
    if (seg  == 1){
        connecting1[c_cl1] = new_conn;
        //printf("New connect%d  %d \n",seg,c_cl1);
        c_cl1++;
        if(c_cl1 == 10) c_cl1 = 0;
    }
    else if(seg == 2){
        connecting2[c_cl2] = new_conn;
        //printf("New connect%d  %d \n",seg,c_cl2);
        c_cl2++;
        if(c_cl2 == 10) c_cl2 = 0;
    }
    else if(seg == 3){
        connecting3[c_cl3] = new_conn;
        //printf("New connect%d  %d \n",seg,c_cl3);
        c_cl3++;
        if(c_cl3 == 10) c_cl3 = 0;
    }
}

void* control_connect(void *args){
    Arg_th *arg = (Arg_th*) args;
    unsigned long long *rec_buf = malloc(sizeof(unsigned long long)); 

    rec_buf[0] = 0;
    struct sockaddr_in  cliaddr;
    int input_byte = 0;
    while(1){        
        socklen_t len = sizeof(cliaddr);

        if (arg->Seg == 1)
            input_byte = recvfrom(soc_connect1, rec_buf, sizeof(rec_buf), 0, (struct sockaddr*)&cliaddr, &len);
        else if (arg->Seg == 2)
            input_byte = recvfrom(soc_connect2, rec_buf, sizeof(rec_buf), 0, (struct sockaddr*)&cliaddr, &len);
        else if (arg->Seg == 3)
            input_byte = recvfrom(soc_connect3, rec_buf, sizeof(rec_buf), 0, (struct sockaddr*)&cliaddr, &len);
        if (input_byte != -1){
            add_connected(cliaddr, arg->Seg);
            //printf("%ld \n", sizeof(connecting1)/sizeof(connecting1[0]));
        }        
        //sleep(1);
    }
}



void* mailing_sequence(void *args){
    Arg_th *arg = (Arg_th*) args;
    int count; 
    unsigned long long *rec_buf =malloc(sizeof(unsigned long long)); 

    if (arg->Seg == 1)
        while (1)
        {
            count = sizeof(connecting1)/ sizeof(connecting1[0]);

            for(int i =0 ; i < count;i++){        
                pthread_mutex_lock(&mutex);
                rec_buf[0]=Current_segment[arg->Seg-1].curr;
                socklen_t len = sizeof(connecting1[i]);
                sendto(soc_connect1, rec_buf, sizeof(rec_buf), 0, (struct sockaddr*)&connecting1[i], len);
                pthread_mutex_unlock(&mutex);
                
            }
            sleep(2);
        }    
    else if (arg->Seg == 2)
        while (1)
        {
            count = sizeof(connecting2)/ sizeof(connecting2[0]);

            for(int i =0 ; i < count;i++){        
                pthread_mutex_lock(&mutex);
                rec_buf[0]=Current_segment[arg->Seg-1].curr;
                socklen_t len = sizeof(connecting2[i]);
                sendto(soc_connect2, rec_buf, sizeof(rec_buf), 0, (struct sockaddr*)&connecting2[i], len);
                pthread_mutex_unlock(&mutex);
                
            }
            sleep(2);
        }    
    else if (arg->Seg == 3)
        while (1)
        {
            count = sizeof(connecting3)/ sizeof(connecting3[0]);

            for(int i =0 ; i < count;i++){        
                pthread_mutex_lock(&mutex);
                rec_buf[0]=Current_segment[arg->Seg-1].curr;
                socklen_t len = sizeof(connecting3[i]);
                sendto(soc_connect3, rec_buf, sizeof(rec_buf), 0, (struct sockaddr*)&connecting3[i], len);
                pthread_mutex_unlock(&mutex);
                
            }
            sleep(2);
        }    
}

int main(void){
    printf("Hello, i'm server\n");

    pthread_t thread_Segment1, thread_Segment2,thread_Segment3, thread_calculate, 
    thread_conn1, thread_conn2, thread_conn3, 
    thread_mailing1,thread_mailing2,thread_mailing3;
    int status_Segment1, status_Segment2,status_Segment3, Status_calculate, Status_con, Status_mailing;
    int status_addr_calculate;

    Arg_th seg[3] = {1,2,3};

    pthread_mutex_init(&mutex, NULL);
    
    Start_new_thread(status_Segment1, &thread_Segment1,Input_segment, (void*)&seg[0]);
    pthread_detach(thread_Segment1);

    Start_new_thread(status_Segment2, &thread_Segment2,Input_segment, (void*)&seg[1]);
    pthread_detach(thread_Segment2);

    Start_new_thread(status_Segment3, &thread_Segment3,Input_segment, (void*)&seg[2]);
    pthread_detach(thread_Segment3);
    
    Start_new_thread(Status_calculate,&thread_calculate, Calculate_sequence, NULL);
    //pthread_detach(thread_calculate);

    soc_connect1 = Create_socket(5004, &serv_connect[0]);
    serv_connect[0].sin_addr.s_addr = htonl(INADDR_ANY);//в локальную петлю
    bind(soc_connect1, (struct sockaddr*)&serv_connect[0], sizeof(serv_connect[0]));

    soc_connect2 = Create_socket(5005, &serv_connect[1]);
    serv_connect[1].sin_addr.s_addr = htonl(INADDR_ANY);//в локальную петлю
    bind(soc_connect2, (struct sockaddr*)&serv_connect[1], sizeof(serv_connect[1]));

    soc_connect3 = Create_socket(5006, &serv_connect[2]);
    serv_connect[2].sin_addr.s_addr = htonl(INADDR_ANY);//в локальную петлю
    bind(soc_connect3, (struct sockaddr*)&serv_connect[2], sizeof(serv_connect[2]));    
    
    Start_new_thread(Status_con,&thread_conn1,control_connect,(void*)&seg[0]);
    pthread_detach(thread_conn1);

    Start_new_thread(Status_con,&thread_conn2,control_connect,(void*)&seg[1]);
    pthread_detach(thread_conn2);

    Start_new_thread(Status_con,&thread_conn3,control_connect,(void*)&seg[2]);
    pthread_detach(thread_conn3);

    Start_new_thread(Status_mailing, &thread_mailing1,mailing_sequence, (void*)&seg[0]);
    pthread_detach(thread_mailing1);

    Start_new_thread(Status_mailing, &thread_mailing2,mailing_sequence, (void*)&seg[1]);
    pthread_detach(thread_mailing2);

    Start_new_thread(Status_mailing, &thread_mailing3,mailing_sequence, (void*)&seg[2]);
    pthread_detach(thread_mailing3);

    //pthread_detach(pthread_self());

    Status_calculate = pthread_join(thread_calculate, (void**)&status_addr_calculate);
    if (Status_calculate != SUCCESS) {
        printf("error: can't join thread, status = %d\n", Status_calculate);
        exit(ERROR_JOIN_THREAD);
    }
    
    return 0;
}

