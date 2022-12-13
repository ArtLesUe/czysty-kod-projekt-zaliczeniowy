require("dotenv").config();

describe('POST /api/task/add (bad request)', () => {
  test('http status code: 400, http status text: Bad Request', async () => {
    response = await fetch(process.env.TEST_API_URL + '/api/task/add', {
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

describe('POST /api/task/add (no token)', () => {
  test('http status code: 401, http status text: Unauthorized, response.status: wrong-auth', async () => {
    new_task = {
      "Title": Math.random().toString(36),
      "Description": Math.random().toString(36)
    }; 

    response = await fetch(process.env.TEST_API_URL + '/api/task/add', {
      method: 'POST',
      headers: {
        'Content-type': 'application/json'
      },
      body: JSON.stringify(new_task)
    });
    
    response_json = await response.json();

    expect(response.status).toBe(401); 
    expect(response.statusText).toBe('Unauthorized'); 
    expect(response_json.status).toEqual('wrong-auth');
  });
});

describe('POST /api/task/add (bad token)', () => {
  test('http status code: 401, http status text: Unauthorized, response.status: wrong-auth', async () => {
    new_task = {
      "Title": Math.random().toString(36),
      "Description": Math.random().toString(36)
    }; 

    response = await fetch(process.env.TEST_API_URL + '/api/task/add', {
      method: 'POST',
      headers: {
        'Content-type': 'application/json',
        token: 'bad-token'
      },
      body: JSON.stringify(new_task)
    });
    
    response_json = await response.json();

    expect(response.status).toBe(401); 
    expect(response.statusText).toBe('Unauthorized'); 
    expect(response_json.status).toEqual('auth-failed');
  });
});

describe('POST /api/task/add (good token, bad all data)', () => {
  test('http status code: 422, http status text: Unprocessable Entity, response.status: wrong-data', async () => {
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

    new_task = {
      "Title": null,
      "Description": null
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

    expect(response.status).toBe(422); 
    expect(response.statusText).toBe('Unprocessable Entity'); 
    expect(response_json.status).toEqual('wrong-data');
  });
});

describe('POST /api/task/add (good token, good data)', () => {
  test('http status code: 201, http status text: Created, response.status: OK, task: exists', async () => {
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

    response = await fetch(process.env.TEST_API_URL + '/api/tasks/list', {
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
      return obj.title === new_task.Title
    });

    expect(search.length == 1).toBeTruthy();
  });
});