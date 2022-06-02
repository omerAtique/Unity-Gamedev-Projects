WINDOW_WIDTH = 1280
WINDOW_HEIGHT = 720

VIRTUAL_WIDTH = 432
VIRTUAL_HEIGHT = 243

PADDLE_SPEED = 200

Class = require 'class'
push = require 'push'

require 'Ball'
require 'Paddle'

function love.load()
    math.randomseed(os.time())
    love.graphics.setDefaultFilter('nearest','nearest')

    smallFont = love.graphics.newFont('aAtmospheric.ttf', 8)
    scoreFont = love.graphics.newFont('aAtmospheric.ttf', 35)
    victoryFont = love.graphics.newFont('aAtmospheric.ttf', 20)
   
    sounds =
     {
        ['paddle_hit'] = love.audio.newSource('paddle_hit.wav', 'static'),
        ['point_scored'] = love.audio.newSource('point_scored.wav', 'static'),
        ['wall_hit'] = love.audio.newSource('wall_hit.wav', 'static')
     }

    paddle1 = Paddle(5, 20, 5, 20)
    paddle2 = Paddle(VIRTUAL_WIDTH - 10, VIRTUAL_HEIGHT - 30, 5, 20)
    
    ball = Ball(VIRTUAL_WIDTH / 2 - 2, VIRTUAL_HEIGHT / 2 - 2 , 5, 5)

    player1Score = 0
    player2Score = 0

    servingPlayer = math.random(2) == 1 and 2

    winningPlayer = math.random(2) == 1 and 2

    player1Y = 30
    player2Y = VIRTUAL_HEIGHT - 40 

   
    gameState = 'start'
  

    push:setupScreen(VIRTUAL_WIDTH, VIRTUAL_HEIGHT, WINDOW_WIDTH, WINDOW_HEIGHT, {
        fullscreen = false,
        resizable = false,
        vsync = true
    })
end
 
function love.keypressed(key)
    if key == 'escape' then
        love.event.quit()
    elseif key == 'space' then
        if gameState == 'start' then
            gameState = 'play'
        elseif gameState == 'play' then
            gameState = 'start'

            ball:reset()
        end
    end
end

function love.update(dt)
    if  gameState == 'play' then

         if ball.x <= 0 then
            player2Score = player2Score + 1
            servingPlayer = 1
            ball:reset()
            ball.dx = 100
            
            sounds['point_scored']:play()
            
            if player2Score >= 3 then
                gameState = 'victory'
                winningPlayer = 2
            else
                gameState = 'start'
            end
         end

         if ball.x >= VIRTUAL_WIDTH - 4 then
            player1Score = player1Score + 1
            servingPlayer = 2
            ball:reset()
            ball.dx = -100

            sounds['point_scored']:play()

            if player1Score >= 3 then
                gameState = 'victory'
                winningPlayer = 1
            else
                gameState = 'start'
            end
         end
    end

    if ball:collides(paddle1) then
        ball.dx = -ball.dx

        sounds['paddle_hit']:play()
        
        
    end

    if ball:collides(paddle2) then
        ball.dx = -ball.dx

        sounds['paddle_hit']:play()
    end

    if ball.y <= 0 then
        ball.dy = -ball.dy
        ball.y = 0
        sounds['wall_hit']:play()
    end

    if ball.y >= VIRTUAL_HEIGHT - 4 then
        ball.dy = -ball.dy
        ball.y = VIRTUAL_HEIGHT - 4

        sounds['wall_hit']:play()
    end

    

    paddle1:update(dt)
    paddle2:update(dt)

    if love.keyboard.isDown('w') then
        paddle1.dy = -PADDLE_SPEED
    elseif love.keyboard.isDown('s') then
        paddle1.dy = PADDLE_SPEED
    else
        paddle1.dy = 0

    end

    if love.keyboard.isDown('up') then
        paddle2.dy = -PADDLE_SPEED
    elseif love.keyboard.isDown('down') then
        paddle2.dy = PADDLE_SPEED
    else 
        paddle2.dy = 0
    end

    if gameState == 'play' then
        ball:update(dt)
    end
end



function love.draw()
    push:apply('start')

    love.graphics.clear(10/ 255, 10/ 255, 10 / 55, 255 /255)
    
    paddle1:render()
    paddle2:render()
    ball:render()

    love.graphics.setFont(smallFont)
    
    
    if gameState == 'start' then
        love.graphics.printf('WELCOME TO PONG!', 0, 20, VIRTUAL_WIDTH, 'center')
        love.graphics.printf('HELLO START STATE', 0, 40, VIRTUAL_WIDTH, 'center') 
        love.graphics.printf('PRESS SPACE TO SERVE', 0 , 200, VIRTUAL_WIDTH, 'center')
        love.graphics.printf("player ".. tostring(servingPlayer).."'s turn!", 0, 220, VIRTUAL_WIDTH, 'center')
        love.graphics.setFont(scoreFont)
        love.graphics.print(player1Score, VIRTUAL_WIDTH / 2 - 50, VIRTUAL_HEIGHT / 3)
        love.graphics.print(player2Score, VIRTUAL_WIDTH / 2 + 15, VIRTUAL_HEIGHT / 3)
        
    elseif gameState == 'play' then
        love.graphics.printf('WELCOME TO PONG!', 0, 20, VIRTUAL_WIDTH, 'center')
        love.graphics.printf('HELLO PLAY STATE', 0, 40, VIRTUAL_WIDTH, 'center')
        love.graphics.setFont(scoreFont)
        love.graphics.print(player1Score, VIRTUAL_WIDTH / 2 - 50, VIRTUAL_HEIGHT / 3)
        love.graphics.print(player2Score, VIRTUAL_WIDTH / 2 + 15, VIRTUAL_HEIGHT / 3)
        
    elseif gameState == 'victory' then
        love.graphics.setFont(victoryFont)
        love.graphics.printf('PRESS SPACE TO SERVE', 0 , 200, VIRTUAL_WIDTH, 'center')
        love.graphics.printf('Player '.. tostring(winningPlayer).. " WINS!!!!! T-T", 0, 40, VIRTUAL_WIDTH, 'center')
    end

    displayFPS()

    push:apply('end')
end
    
function displayFPS()
    love.graphics.setColor(0, 1 , 0, 1)
    love.graphics.setFont(smallFont)
    love.graphics.print('FPS: ' .. tostring(love.timer.getFPS()), 40, 20)
    love.graphics.setColor(1, 1, 1, 1)
end 