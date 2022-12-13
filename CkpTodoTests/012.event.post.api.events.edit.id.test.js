require("dotenv").config();

describe('POST /api/events/edit/{id} (bad request)', () => {
  test('http status code: 400, http status text: Bad Request', async () => {
    response = await fetch(process.env.TEST_API_URL + '/api/events/edit/0', {
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

describe('POST /api/events/edit/{id} (no token)', () => {
  test('http status code: 401, http status text: Unauthorized, response.status: wrong-auth', async () => {
    new_task = {
      "Id": 0,
      "Title": Math.random().toString(36),
      "Description": Math.random().toString(36),
      "StartDate": "2022-12-01 00:00:00",
      "EndDate": "2022-12-02 00:00:00"
    }; 

    response = await fetch(process.env.TEST_API_URL + '/api/events/edit/0', {
      method: 'POST',
      headers: {
        'Content-type': 'application/json'
      },
      body: JSON.stringify(new_task)
    });
    
    response_json = await response.json();

    expect(response.status).toBe(401); 
    expect(response.statusText).toBe('Unauthorized'); 
    expect(response_json.status).toEqual('auth-failed');
  });
});

describe('POST /api/events/edit/{id} (bad token)', () => {
  test('http status code: 401, http status text: Unauthorized, response.status: wrong-auth', async () => {
    new_task = {
      "Id": 0,
      "Title": Math.random().toString(36),
      "Description": Math.random().toString(36),
      "StartDate": "2022-12-01 00:00:00",
      "EndDate": "2022-12-02 00:00:00"
    }; 

    response = await fetch(process.env.TEST_API_URL + '/api/events/edit/0', {
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

describe('POST /api/events/edit/{id} (good token, bad all data)', () => {
  test('http status code: 201, http status text: Created, response.status: OK', async () => {
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
      "Id": 0,
      "Title": null,
      "Description": null,
      "StartDate": null,
      "EndDate": null
    }; 

    response = await fetch(process.env.TEST_API_URL + '/api/events/edit/0', {
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
  });
});

describe('POST /api/events/edit/{id} (good token, not-existing)', () => {
  test('http status code: 201, http status text: Created, response.status: OK', async () => {
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
      "Id": 0,
      "Title": Math.random().toString(36),
      "Description": Math.random().toString(36),
      "StartDate": "2022-12-01 00:00:00",
      "EndDate": "2022-12-02 00:00:00"
    }; 

    response = await fetch(process.env.TEST_API_URL + '/api/events/edit/0', {
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
  });
});

describe('POST /api/events/edit/{id} (good token, good data)', () => {
  test('http status code: 201, http status text: Created, response.status: OK, task: modified', async () => {
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
      "Id": 0,
      "Title": Math.random().toString(36),
      "Description": Math.random().toString(36),
      "StartDate": "2022-12-01 00:00:00",
      "EndDate": "2022-12-02 00:00:00"
    }; 

    response = await fetch(process.env.TEST_API_URL + '/api/events/add', {
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

    response = await fetch(process.env.TEST_API_URL + '/api/events/list', {
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

    new_task_2 = {
      "Id": 0,
      "Title": Math.random().toString(36),
      "Description": Math.random().toString(36),
      "StartDate": "2022-12-01 00:00:00",
      "EndDate": "2022-12-02 00:00:00"
    }; 

    response = await fetch(process.env.TEST_API_URL + '/api/events/edit/' + search[0].id.toString(), {
      method: 'POST',
      headers: {
        'Content-type': 'application/json',
        token: auth_token
      },
      body: JSON.stringify(new_task_2)
    });
    
    response_json = await response.json();

    expect(response.status).toBe(201); 
    expect(response.statusText).toBe('Created'); 
    expect(response_json.status).toEqual('OK');

    response = await fetch(process.env.TEST_API_URL + '/api/events/list', {
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

    search_2 = response_json.filter(obj => {
      return obj.title === new_task_2.Title
    });

    expect(search_2.length == 1).toBeTruthy();
    expect(search[0].id == search_2[0].id);
  });
});