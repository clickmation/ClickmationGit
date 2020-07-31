#include <iostream>
#include <queue>

using namespace std;
int map[101][101];
int visit[101];
int n, cnt;

void bfs(int start) {
    visit[start] = 1;
    queue<int> q;
    q.push(start);
    while (!q.empty()) {
        int node = q.front();
        q.pop();

        for (int i = 1; i <= n; i++) {
            if (!visit[i] && map[node][i] == 1) {
                q.push(i);
                visit[i] = 1;
                cnt++;
            }
        }
    }
}

int main() {
    int con;
    int u, v;
    cin >> n;
    cin >> con;

    for (int i = 0; i < con; i++){
        cin >> u >> v;
        map[u][v] = map[v][u] = 1;
    }

    bfs(1);
    cout << cnt << endl;
}
