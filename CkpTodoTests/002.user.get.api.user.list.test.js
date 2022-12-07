require("dotenv").config();

describe('GET /api/user/list (no token)', () => {
  test('http status code: 401, http status text: Unauthorized, responseBody = []', async () => {
    response = await fetch(process.env.TEST_API_URL + '/api/user/list', {
      method: 'GET',
      headers: {
        'Content-type': 'application/json'
      }
    });
    
    response_json = await response.json();

    expect(response.status).toBe(401); 
    expect(response.statusText).toBe('Unauthorized'); 
    expect(response_json).toEqual([]);
  });
});

describe('GET /api/user/list (bad token)', () => {
  test('http status code: 401, http status text: Unauthorized, responseBody = []', async () => {
    response = await fetch(process.env.TEST_API_URL + '/api/user/list', {
      method: 'GET',
      headers: {
        'Content-type': 'application/json',
        'token': 'bad-token'
      }
    });
    
    response_json = await response.json();

    expect(response.status).toBe(401); 
    expect(response.statusText).toBe('Unauthorized'); 
    expect(response_json).toEqual([]);
  });
});

describe('GET /api/user/list (good token)', () => {
  test('http status code: 200, http status text: OK, length(responseBody) > 0, user[0].name-surname: Administrator Systemu', async () => {
    response = await fetch(process.env.TEST_API_URL + '/api/user/login', {
      method: 'POST',
      headers: {
        'Content-type': 'application/json'
      },
      body: JSON.stringify({
        "Login": "admin@admin.pl",
        "Password": "admin123"
      })
    });
    
    response_json = await response.json();

    expect(response.status).toBe(200); 
    expect(response.statusText).toBe('OK'); 
    expect(response_json.userId).toBe(1);
    expect(response_json.token).not.toBe(null);

    auth_token = response_json.token;

    response = await fetch(process.env.TEST_API_URL + '/api/user/list', {
      method: 'GET',
      headers: {
        'Content-type': 'application/json',
        'token': auth_token
      }
    });
    
    response_json = await response.json();

    expect(response.status).toBe(200); 
    expect(response.statusText).toBe('OK'); 
    expect(response_json.length > 0).toBeTruthy();
    expect(response_json[0].name).toBe('Administrator');
    expect(response_json[0].surname).toBe('Systemu');
  });
});