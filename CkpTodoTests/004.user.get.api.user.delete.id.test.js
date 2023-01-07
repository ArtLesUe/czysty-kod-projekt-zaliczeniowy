require("dotenv").config();

describe('DELETE /api/user/delete/{id} (no token)', () => {
  test('http status code: 401, http status text: Unauthorized, response = { "status": "auth-failed" }', async () => {
    response = await fetch(process.env.TEST_API_URL + '/api/user/delete/0', {
      method: 'DELETE',
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

describe('DELETE /api/user/delete/{id} (bad token)', () => {
  test('http status code: 401, http status text: Unauthorized, response = { "status": "auth-failed" }', async () => {
    response = await fetch(process.env.TEST_API_URL + '/api/user/delete/0', {
      method: 'DELETE',
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

describe('DELETE /api/user/delete/{id} (good token, not existing user)', () => {
  test('http status code: 406, http status text: Not Acceptable, response = { "status": "deleting-not-existing-forbidden" }', async () => {
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

    response = await fetch(process.env.TEST_API_URL + '/api/user/delete/0', {
      method: 'DELETE',
      headers: {
        'Content-type': 'application/json',
        token: auth_token
      }
    });
    
    response_json = await response.json();

    expect(response.status).toBe(406); 
    expect(response.statusText).toBe('Not Acceptable'); 
    expect(response_json).toEqual({ "status": "deleting-not-existing-forbidden" });
  });
});

describe('DELETE /api/user/delete/{id} (good token, self deletion)', () => {
  test('http status code: 406, http status text: Not Acceptable, response = { "status": "self-deletion-forbidden" }', async () => {
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

    response = await fetch(process.env.TEST_API_URL + '/api/user/delete/1', {
      method: 'DELETE',
      headers: {
        'Content-type': 'application/json',
        token: auth_token
      }
    });
    
    response_json = await response.json();

    expect(response.status).toBe(406); 
    expect(response.statusText).toBe('Not Acceptable'); 
    expect(response_json).toEqual({ "status": "self-deletion-forbidden" });
  });
});

describe('DELETE /api/user/delete/{id} (good token, existing user, not logged, deleted success)', () => {
  test('http status code: 200, http status text: OK, response = { "status": "deleted" }', async () => {
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
      "Name": Math.random().toString(36),
      "Surname": Math.random().toString(36),
      "Email": Math.random().toString(36) + '@' + Math.random().toString(36) + '.pl',
      "Password": Math.random().toString(36)
    };

    response = await fetch(process.env.TEST_API_URL + '/api/user/register', {
      method: 'POST',
      headers: {
        'Content-type': 'application/json',
        'token': auth_token
      },
      body: JSON.stringify(new_user)
    });
    
    response_json = await response.json();

    expect(response.status).toBe(201); 
    expect(response.statusText).toBe('Created'); 
    expect(response_json.status).toEqual('OK');

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

    search = response_json.filter(obj => {
      return obj.email === new_user.Email
    });

    expect(search.length == 1).toBeTruthy();

    response = await fetch(process.env.TEST_API_URL + '/api/user/delete/' + search[0].id.toString(), {
      method: 'DELETE',
      headers: {
        'Content-type': 'application/json',
        token: auth_token
      }
    });
    
    response_json = await response.json();

    expect(response.status).toBe(200); 
    expect(response.statusText).toBe('OK'); 
    expect(response_json).toEqual({ "status": "deleted" });

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

    search = response_json.filter(obj => {
      return obj.email === new_user.Email
    });

    expect(search.length == 0).toBeTruthy();
  });
});