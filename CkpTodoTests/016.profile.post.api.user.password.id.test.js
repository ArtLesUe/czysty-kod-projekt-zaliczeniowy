require("dotenv").config();

describe('PATCH /api/user/password/{id} (no token)', () => {
  test('http status code: 401, http status text: Unauthorized', async () => {
    response = await fetch(process.env.TEST_API_URL + '/api/user/password/1', {
      method: 'PATCH',
      headers: {
        'Content-type': 'application/json'
      },
      body: JSON.stringify({})
    });
    
    response_json = await response.json();

    expect(response.status).toBe(401); 
    expect(response.statusText).toBe('Unauthorized'); 
    expect(response_json).toEqual({ "status": "auth-failed" });
  });
});

describe('PATCH /api/user/password/{id} (bad token)', () => {
  test('http status code: 401, http status text: Unauthorized', async () => {
    response = await fetch(process.env.TEST_API_URL + '/api/user/password/1', {
      method: 'PATCH',
      headers: {
        'Content-type': 'application/json',
        'token': 'bad-token'
      },
      body: JSON.stringify({})
    });
    
    response_json = await response.json();

    expect(response.status).toBe(401); 
    expect(response.statusText).toBe('Unauthorized'); 
    expect(response_json).toEqual({ "status": "auth-failed" });
  });
});

describe('PATCH /api/user/password/{id} (good token)', () => {
  test('http status code: 200, http status text: OK, edited: true', async () => {
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

    new_user = {
      "Password": 'admin123'
    };

    response = await fetch(process.env.TEST_API_URL + '/api/user/password/1', {
      method: 'PATCH',
      headers: {
        'Content-type': 'application/json',
        'token': auth_token
      },
      body: JSON.stringify(new_user)
    });
    
    response_json = await response.json();

    expect(response.status).toBe(201); 
    expect(response.statusText).toBe('Created'); 
    expect(response_json).toEqual({ "status": "OK" });

    response = await fetch(process.env.TEST_API_URL + '/api/user/login', {
      method: 'POST',
      headers: {
        'Content-type': 'application/json'
      },
      body: JSON.stringify({
        "Login": "admin@admin.pl",
        "Password": new_user.Password
      })
    });
    
    response_json = await response.json();

    expect(response.status).toBe(200); 
    expect(response.statusText).toBe('OK'); 
    expect(response_json.userId).toBe(1);
    expect(response_json.token).not.toBe(null);

    auth_token = response_json.token;

    response = await fetch(process.env.TEST_API_URL + '/api/user/password/1', {
      method: 'PATCH',
      headers: {
        'Content-type': 'application/json',
        'token': auth_token
      },
      body: JSON.stringify({ Password: 'admin123' })
    });
    
    response_json = await response.json();

    expect(response.status).toBe(201); 
    expect(response.statusText).toBe('Created'); 
    expect(response_json).toEqual({ "status": "OK" });

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