import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    stages: [
        { duration: '30s', target: 10 },  // 10 usuários
        { duration: '1m', target: 50 },   // 50 usuários
        { duration: '30s', target: 0 },   // ramp-down
    ],
};

export default function () {
    let res = http.get('http://localhost:8080/api/customers');
    check(res, { 'status is 200': (r) => r.status === 200 });
    sleep(1);
}
