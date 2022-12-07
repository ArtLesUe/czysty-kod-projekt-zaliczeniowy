require("dotenv").config();

describe('POST /api/user/login (wrong request data)', () => {
  test('http status code: 400, http status text: Bad Request', async () => {
    response = await fetch(process.env.TEST_API_URL + '/api/user/login', {
      method: 'POST',
      headers: {
        'Content-type': 'application/json'
      }
    });
    
    response_json = await response.json();

    expect(response.status).toBe(400); 
    expect(response.statusText).toBe('Bad Request'); 
  });
});

describe('POST /api/user/login (empty request data)', () => {
  test('http status code: 422, http status text: Unprocessable Entity, userId = 0, token = null', async () => {
    response = await fetch(process.env.TEST_API_URL + '/api/user/login', {
      method: 'POST',
      headers: {
        'Content-type': 'application/json'
      },
      body: JSON.stringify({
        "Login": "",
        "Password": ""
      })
    });
    
    response_json = await response.json();

    expect(response.status).toBe(422); 
    expect(response.statusText).toBe('Unprocessable Entity'); 
    expect(response_json.userId).toBe(0);
    expect(response_json.token).toBe(null);
  });
});

describe('POST /api/user/login (wrong login data)', () => {
  test('http status code: 401, http status text: Unauthorized, userId = 0, token = null', async () => {
    response = await fetch(process.env.TEST_API_URL + '/api/user/login', {
      method: 'POST',
      headers: {
        'Content-type': 'application/json'
      },
      body: JSON.stringify({
        "Login": "test@test.pl",
        "Password": "test"
      })
    });
    
    response_json = await response.json();

    expect(response.status).toBe(401); 
    expect(response.statusText).toBe('Unauthorized'); 
    expect(response_json.userId).toBe(0);
    expect(response_json.token).toBe(null);
  });
});

describe('POST /api/user/login (good login data)', () => {
  test('http status code: 200, http status text: OK, userId = 1, token != null', async () => {
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
  });
});