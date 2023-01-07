require("dotenv").config();

describe('PATCH /api/task/check/{id} (no token)', () => {
  test('http status code: 401, http status text: Unauthorized, response = { "status": "auth-failed" }', async () => {
    response = await fetch(process.env.TEST_API_URL + '/api/task/check/0', {
      method: 'PATCH',
      headers: {
        'Content-type': 'application/json'
      }
    });
    
    response_json = await response.json();

    expect(response.status).toBe(401); 
    expect(response.statusText).toBe('Unauthorized'); 
    expect(response_json).toEqual({ "status": "auth-failed" });
  });
});

describe('PATCH /api/task/check/{id} (bad token)', () => {
  test('http status code: 401, http status text: Unauthorized, response = { "status": "auth-failed" }', async () => {
    response = await fetch(process.env.TEST_API_URL + '/api/task/check/0', {
      method: 'PATCH',
      headers: {
        'Content-type': 'application/json',
        token: 'bad-token'
      }
    });
    
    response_json = await response.json();

    expect(response.status).toBe(401); 
    expect(response.statusText).toBe('Unauthorized'); 
    expect(response_json).toEqual({ "status": "auth-failed" });
  });
});

describe('PATCH /api/task/check/{id} (good token, not existing task)', () => {
  test('http status code: 200, http status text: OK, response = { "status": "checked" }', async () => {
    response = await fetch(process.env.TEST_API_URL + '/api/user/login', {
      method: 'PATCH',
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

    response = await fetch(process.env.TEST_API_URL + '/api/task/check/0', {
      method: 'PATCH',
      headers: {
        'Content-type': 'application/json',
        token: auth_token
      }
    });
    
    response_json = await response.json();

    expect(response.status).toBe(200); 
    expect(response.statusText).toBe('OK'); 
    expect(response_json).toEqual({ "status": "checked" });
  });
});

describe('PATCH /api/task/check/{id} (good token, existing task, check success)', () => {
  test('http status code: 200, http status text: OK, response = { "status": "checked" }', async () => {
    response = await fetch(process.env.TEST_API_URL + '/api/user/login', {
      method: 'PATCH',
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

    new_task = {
      "Title": Math.random().toString(36),
      "Description": Math.random().toString(36)
    }; 

    response = await fetch(process.env.TEST_API_URL + '/api/task/add', {
      method: 'POST',
      headers: {
        'Content-type': 'application/json',
        token: auth_token
      },
      body: JSON.stringify(new_task)
    });
    
    response_json = await response.json();

    expect(response.status).toBe(201); 
    expect(response.statusText).toBe('Created'); 
    expect(response_json.status).toEqual('OK');

    response = await fetch(process.env.TEST_API_URL + '/api/task/list', {
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

    search = response_json.filter(obj => {
      return obj.title === new_task.Title && obj.isChecked == false
    });

    expect(search.length == 1).toBeTruthy();

    response = await fetch(process.env.TEST_API_URL + '/api/task/check/' + search[0].id.toString(), {
      method: 'PATCH',
      headers: {
        'Content-type': 'application/json',
        token: auth_token
      }
    });
    
    response_json = await response.json();

    expect(response.status).toBe(200); 
    expect(response.statusText).toBe('OK'); 
    expect(response_json).toEqual({ "status": "checked" });

    response = await fetch(process.env.TEST_API_URL + '/api/task/list', {
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

    search = response_json.filter(obj => {
      return obj.title === new_task.Title && obj.isChecked == true
    });

    expect(search.length == 1).toBeTruthy();
  });
});