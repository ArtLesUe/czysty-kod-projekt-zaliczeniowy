require("dotenv").config();

describe('POST /api/user/register (bad request)', () => {
  test('http status code: 400, http status text: Bad Request', async () => {
    response = await fetch(process.env.TEST_API_URL + '/api/user/register', {
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

describe('POST /api/user/register (no token)', () => {
  test('http status code: 401, http status text: Unauthorized, response.status: auth-failed', async () => {
    response = await fetch(process.env.TEST_API_URL + '/api/user/register', {
      method: 'POST',
      headers: {
        'Content-type': 'application/json'
      },
      body: JSON.stringify({
        "Name": "Jan",
        "Surname": "Kowalski",
        "Email": "admin@admin.pl",
        "Password": "admin123"
      })
    });
    
    response_json = await response.json();

    expect(response.status).toBe(401); 
    expect(response.statusText).toBe('Unauthorized'); 
    expect(response_json.status).toEqual('auth-failed');
  });
});

describe('POST /api/user/register (bad token)', () => {
  test('http status code: 401, http status text: Unauthorized, response.status: auth-failed', async () => {
    response = await fetch(process.env.TEST_API_URL + '/api/user/register', {
      method: 'POST',
      headers: {
        'Content-type': 'application/json',
        'token': 'bad-token'
      },
      body: JSON.stringify({
        "Name": "Jan",
        "Surname": "Kowalski",
        "Email": "admin@admin.pl",
        "Password": "admin123"
      })
    });
    
    response_json = await response.json();

    expect(response.status).toBe(401); 
    expect(response.statusText).toBe('Unauthorized'); 
    expect(response_json.status).toEqual('auth-failed');
  });
});

describe('POST /api/user/register (good token, bad all data)', () => {
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

    response = await fetch(process.env.TEST_API_URL + '/api/user/register', {
      method: 'POST',
      headers: {
        'Content-type': 'application/json',
        'token': auth_token
      },
      body: JSON.stringify({
        "Name": null,
        "Surname": null,
        "Email": null,
        "Password": null
      })
    });
    
    response_json = await response.json();

    expect(response.status).toBe(422); 
    expect(response.statusText).toBe('Unprocessable Entity'); 
    expect(response_json.status).toEqual('wrong-data');
  });
});

describe('POST /api/user/register (good token, bad name)', () => {
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

    response = await fetch(process.env.TEST_API_URL + '/api/user/register', {
      method: 'POST',
      headers: {
        'Content-type': 'application/json',
        'token': auth_token
      },
      body: JSON.stringify({
        "Name": null,
        "Surname": Math.random().toString(36),
        "Email": Math.random().toString(36) + '@' + Math.random().toString(36) + '.pl',
        "Password": Math.random().toString(36)
      })
    });
    
    response_json = await response.json();

    expect(response.status).toBe(422); 
    expect(response.statusText).toBe('Unprocessable Entity'); 
    expect(response_json.status).toEqual('wrong-data');
  });
});

describe('POST /api/user/register (good token, bad surname)', () => {
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

    response = await fetch(process.env.TEST_API_URL + '/api/user/register', {
      method: 'POST',
      headers: {
        'Content-type': 'application/json',
        'token': auth_token
      },
      body: JSON.stringify({
        "Name": Math.random().toString(36),
        "Surname": null,
        "Email": Math.random().toString(36) + '@' + Math.random().toString(36) + '.pl',
        "Password": Math.random().toString(36)
      })
    });
    
    response_json = await response.json();

    expect(response.status).toBe(422); 
    expect(response.statusText).toBe('Unprocessable Entity'); 
    expect(response_json.status).toEqual('wrong-data');
  });
});

describe('POST /api/user/register (good token, bad email)', () => {
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

    response = await fetch(process.env.TEST_API_URL + '/api/user/register', {
      method: 'POST',
      headers: {
        'Content-type': 'application/json',
        'token': auth_token
      },
      body: JSON.stringify({
        "Name": Math.random().toString(36),
        "Surname": Math.random().toString(36),
        "Email": Math.random().toString(36),
        "Password": Math.random().toString(36)
      })
    });
    
    response_json = await response.json();

    expect(response.status).toBe(422); 
    expect(response.statusText).toBe('Unprocessable Entity'); 
    expect(response_json.status).toEqual('wrong-data');
  });
});

describe('POST /api/user/register (good token, no email)', () => {
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

    response = await fetch(process.env.TEST_API_URL + '/api/user/register', {
      method: 'POST',
      headers: {
        'Content-type': 'application/json',
        'token': auth_token
      },
      body: JSON.stringify({
        "Name": Math.random().toString(36),
        "Surname": Math.random().toString(36),
        "Email": null,
        "Password": Math.random().toString(36)
      })
    });
    
    response_json = await response.json();

    expect(response.status).toBe(422); 
    expect(response.statusText).toBe('Unprocessable Entity'); 
    expect(response_json.status).toEqual('wrong-data');
  });
});

describe('POST /api/user/register (good token, bad password)', () => {
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

    response = await fetch(process.env.TEST_API_URL + '/api/user/register', {
      method: 'POST',
      headers: {
        'Content-type': 'application/json',
        'token': auth_token
      },
      body: JSON.stringify({
        "Name": Math.random().toString(36),
        "Surname": Math.random().toString(36),
        "Email": Math.random().toString(36) + '@' + Math.random().toString(36) + '.pl',
        "Password": null
      })
    });
    
    response_json = await response.json();

    expect(response.status).toBe(422); 
    expect(response.statusText).toBe('Unprocessable Entity'); 
    expect(response_json.status).toEqual('wrong-data');
  });
});

describe('POST /api/user/register (good token, good data)', () => {
  test('http status code: 201, http status text: Created, response.status: OK, user: exists', async () => {
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
  });
});

describe('POST /api/user/register (good token, good data, duplicated user)', () => {
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

    response = await fetch(process.env.TEST_API_URL + '/api/user/register', {
      method: 'POST',
      headers: {
        'Content-type': 'application/json',
        'token': auth_token
      },
      body: JSON.stringify(new_user)
    });
    
    response_json = await response.json();

    expect(response.status).toBe(422); 
    expect(response.statusText).toBe('Unprocessable Entity'); 
    expect(response_json.status).toEqual('wrong-data');
  });
});