#include <iostream>

using namespace std;

int vi[4] = {0,0,-1,1}, vj[4] = {-1,1,0,0};

void dfs(int graph[52][52], int i, int j) {
    if (graph[i][j] == 0) return;

    graph[i][j] = 0;

    for (int m = 0; m < 4; m++) {
        dfs(graph, i+vi[m], i+vj[m]);
    }
}

int main() {
    int T;

    scanf("%d", &T);

    while(T>0){
        int M, N, K;

        scanf("%d %d %d", &M, &N, &K);

        int graph[52][52] = {0,};

        for (int i = 0; i < K; i++) {
            int x, y;
            scanf("%d %d", &x, &y);
            graph[y+1][x+1] = 1;
        }

        int cnt = 0;

        for (int i = 1; i <= N; i++) {
            for (int j = 1; j <= M; j++) {
                if (graph[i][j] == 1) {
                    dfs(graph, i, j);
                    cnt++
                }
            }
        }

        printf("%d\n", cnt);
        T--;
    }

    return 0;
}
