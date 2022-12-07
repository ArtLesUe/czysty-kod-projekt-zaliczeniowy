require("dotenv").config();

describe('POST /api/user/edit/{id} (no token)', () => {
  test('http status code: 401, http status text: Unauthorized, responseBody = []', async () => {
    response = await fetch(process.env.TEST_API_URL + '/api/user/edit/1', {
      method: 'POST',
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

describe('POST /api/user/edit/{id} (bad token)', () => {
  test('http status code: 401, http status text: Unauthorized, responseBody = []', async () => {
    response = await fetch(process.env.TEST_API_URL + '/api/user/edit/1', {
      method: 'POST',
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

describe('GET /api/user/edit/{id} (good token)', () => {
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
      return obj.name === new_user.Name
    });

    expect(search.length == 1).toBeTruthy();

    response = await fetch(process.env.TEST_API_URL + '/api/user/details/' + search[0].id, {
      method: 'GET',
      headers: {
        'Content-type': 'application/json',
        'token': auth_token
      }
    });
    
    response_json = await response.json();

    expect(response.status).toBe(200); 
    expect(response.statusText).toBe('OK'); 
    expect(response_json).toEqual([{
      "id": search[0].id,
      "name": search[0].name,
      "surname": search[0].surname,
      "email": search[0].email,
      "passwordHashed": "",
      "aboutMe": "",
      "city": "",
      "country": "",
      "university": ""
    }]);

    change_data = {
      "id": search[0].id,
      "name": search[0].name,
      "surname": search[0].surname,
      "email": search[0].email,
      "passwordHashed": "",
      "aboutMe": "O mnie",
      "city": "Katowice",
      "country": "Polska",
      "university": "UE Katowice"
    };

    response = await fetch(process.env.TEST_API_URL + '/api/user/edit/' + search[0].id, {
      method: 'POST',
      headers: {
        'Content-type': 'application/json',
        'token': auth_token
      },
      body: JSON.stringify(change_data)
    });

    response_json = await response.json();

    expect(response.status).toBe(201); 
    expect(response.statusText).toBe('Created'); 

    response = await fetch(process.env.TEST_API_URL + '/api/user/details/' + search[0].id, {
      method: 'GET',
      headers: {
        'Content-type': 'application/json',
        'token': auth_token
      }
    });
    
    response_json = await response.json();

    expect(response.status).toBe(200); 
    expect(response.statusText).toBe('OK'); 
    expect(response_json).toEqual([change_data]);
  });
});