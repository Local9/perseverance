--[[ INTERNAL FUNCTIONS ]]--
function getQueryOutfit(blob)
  local lockHash = string.unpack('<i4', blob, 1)
  local hash = string.unpack('<i4', blob, 9)
  local price = string.unpack('<i4', blob, 17)
  local unk1 = string.unpack('<i4', blob, 25)
  local totalItems = string.unpack('<i4', blob, 33)
  local unk2 = string.unpack('<i4', blob, 41)
  local unk3 = string.unpack('<i4', blob, 49)
  local gxt = string.unpack('z', blob, 57)

  local outfitData = {
    LockHash = lockHash,
    Hash = hash,
    Price = price,
    TotalProps = unk1,
    TotalComponents = totalItems,
    Unk2 = unk2,
    Unk3 = unk3,
    Label = gxt
  }
  return outfitData
end

function getComponent(blob)
  local lockHash = string.unpack('<i4', blob, 1)
  local hash = string.unpack('<i4', blob, 9)
  local locate = string.unpack('<i4', blob, 17)
  local drawable = string.unpack('<i4', blob, 25)
  local texture = string.unpack('<i4', blob, 33)
  local field5 = string.unpack('<i4', blob, 41)
  local componentType = string.unpack('<i4', blob, 49)
  local field7 = string.unpack('<i4', blob, 57)
  local field8 = string.unpack('<i4', blob, 65)
  local gxt = string.unpack('z', blob, 73)

  local myObject = {
    LockHash = lockHash,
    Hash = hash,
    Locate = locate,
    Drawable = drawable,
    Texture = texture,
    Price = field5,
    ComponentType = componentType,
    f_7 = field7,
    f_8 = field8,
    Label = gxt 
  }
  return myObject
end

function getVariant(blob)
    local hash = string.unpack('<i4', blob, 1)
    local enumValue = string.unpack('<i4', blob, 9)
    local componentType = string.unpack('<i4', blob, 17)

    local myObject = {
        Hash = hash,
        EnumValue = enumValue,
        ComponentType = componentType
    }
    return myObject
end

--[[ EXPORTED FUNCTIONS ]]--

function GetShopPedComponent(componentHash)
    local blob = string.rep('\0\0\0\0\0\0\0\0', 9+16)
    Citizen.InvokeNative(0x74C0E2A57EC66760, componentHash, blob)
    local myObject = getComponent(blob)

    return myObject
end

function GetShopPedProp(componentHash)
    local blob = string.rep('\0\0\0\0\0\0\0\0', 9+16)
    Citizen.InvokeNative(0x5D5CAFF661DDF6FC, componentHash, blob)
    local myObject = getComponent(blob)

    return myObject
end

function GetShopPedQueryOutfits(characterType)
  local max = SetupShopPedOutfitQuery(characterType, false)
  if max == 0 then return false end

  local items = {}
  for i=0,max-1 do
    local blob = string.rep('\0\0\0\0\0\0\0\0', 7+16)
    Citizen.InvokeNative(0x6D793F03A631FE56, i, blob) -- it starts from 0... so i-1 (DAMN YOU! STUPID SEXY LUA!)
    local outfitData = getQueryOutfit(blob)
    items[(i+1)] = outfitData
  end

  return items
end

function GetShopPedQueryOutfit(outfitIndex, characterType) -- characterType => 0: michael, 1: franklin, 2: trevor, 3:mpmale, 4:mpfemale
    local max = SetupShopPedOutfitQuery(characterType, false)
    if max == 0 then return false end

    local blob = string.rep('\0\0\0\0\0\0\0\0', 7+16)
    Citizen.InvokeNative(0x6D793F03A631FE56, outfitIndex, blob) -- it starts from 0... so i-1 (DAMN YOU! STUPID SEXY LUA!)
    local outfitData = getQueryOutfit(blob)
    
    return outfitData
end

function GetShopPedOutfit(outfitHash)
    local blob = string.rep('\0\0\0\0\0\0\0\0', 7+16)
    Citizen.InvokeNative(0xB7952076E444979D, outfitHash, blob)
    local outfitData = getQueryOutfit(blob)

    return outfitData
end

function GetShopPedOutfitComponentVariant(outfitHash, slot)
    local blob = string.rep('\0\0\0\0\0\0\0\0', 3+16)
    Citizen.InvokeNative(0x19F2A026EDF0013F, outfitHash, slot, blob)
    local variant = getVariant(blob)

    return variant
end

function GetShopPedOutfitPropVariant(outfitHash, slot)
    local blob = string.rep('\0\0\0\0\0\0\0\0', 3+16)
    Citizen.InvokeNative(0xA9F9C2E0FDE11CBB, outfitHash, slot, blob)
    local variant = getVariant(blob)

    return variant
end

function GetShopPedQueryComponent(componentId, componentType, characterType)
    local blob = string.rep('\0\0\0\0\0\0\0\0', 9+16)
    Citizen.InvokeNative(0x1E8C308FD312C036, blob)
    
    local max = SetupShopPedApparelQueryTu(characterType, 0, -1, 0 --[[0=component/1=props]], -1, componentType)
    
    if componentId > max then return false end

    Citizen.InvokeNative(0x249E310B2D920699, componentId, blob)
    local myObject = getComponent(blob)
    return myObject
end

function GetShopPedQueryComponents(componentType, characterType, locate)
    if locate == nil then locate = -1 end
    local blob = string.rep('\0\0\0\0\0\0\0\0', 9+16)
    Citizen.InvokeNative(0x1E8C308FD312C036, blob)
    
    local max = SetupShopPedApparelQueryTu(characterType, 0, locate, 0 --[[0=component/1=props]], -1--[[prop related?]], componentType)
    
    if max == 0 then return false end

    local items = {}

    for i=0,max-1 do
        Citizen.InvokeNative(0x249E310B2D920699, i, blob)
        items[(i+1)] = getComponent(blob)
    end

    return items
end

function QueryGetComponentIndex(nameHash, characterType, componentType)
    local blob = string.rep('\0\0\0\0\0\0\0\0', 9+16)
    Citizen.InvokeNative(0x1E8C308FD312C036, blob)
    
    local max = SetupShopPedApparelQueryTu(characterType, 0, -1, 0 --[[0=component/1=props]], -1--[[prop related?]], componentType)
    if max == 0 then return -1 end

    for i=0,max-1 do
        Citizen.InvokeNative(0x249E310B2D920699, i, blob)
        local data = getComponent(blob)
        if data.Hash == nameHash then
            return i
        end
    end

    return -1
end

function GetShopPedQueryProp(propId, characterType)
    local blob = string.rep('\0\0\0\0\0\0\0\0', 9+16)
    Citizen.InvokeNative(0x1E8C308FD312C036, blob)
    
    local max = SetupShopPedApparelQueryTu(characterType, 0, -1, 1 --[[0=component/1=props]], -1--[[prop related?]], -1)
    if propId > max then return false end

    Citizen.InvokeNative(0xDE44A00999B2837D, propId, blob)
    local myObject = getComponent(blob)
    return myObject
end

function GetShopPedQueryProps(characterType)
  local blob = string.rep('\0\0\0\0\0\0\0\0', 9+16)
  Citizen.InvokeNative(0x1E8C308FD312C036, blob)
  
  local max = SetupShopPedApparelQueryTu(characterType, 0, -1, 1 --[[0=component/1=props]], -1--[[prop related?]], -1)
  
  if max == 0 then return false end

  local items = {}

  for i=0,max-1 do
      Citizen.InvokeNative(0xDE44A00999B2837D, i, blob)
      items[(i+1)] = getComponent(blob)
  end

  return items
end

function QueryGetPropIndex(nameHash, characterType)
  local blob = string.rep('\0\0\0\0\0\0\0\0', 9+16)
  Citizen.InvokeNative(0x1E8C308FD312C036, blob)
  
  local max = SetupShopPedApparelQueryTu(characterType, 0, -1, 1 --[[0=component/1=props]], -1--[[prop related?]], -1)
  if max == 0 then return -1 end

  for i=0,max-1 do
      Citizen.InvokeNative(0xDE44A00999B2837D, i, blob)
      local data = getComponent(blob)
      if data.Hash == nameHash then
          return i
      end
  end

  return -1
end


exports('GetShopPedComponent', GetShopPedComponent)                             -- GetShopPedComponent(componentHash)
exports('GetShopPedProp', GetShopPedProp)                                       -- GetShopPedProp(componentHash)
exports('GetShopPedQueryOutfit', GetShopPedQueryOutfit)                         -- GetShopPedQueryOutfits(characterType)
exports('GetShopPedQueryOutfits', GetShopPedQueryOutfits)                       -- GetShopPedQueryOutfit(outfitIndex, characterType) -- characterType => 0: michael, 1: franklin, 2: trevor, 3:mpmale, 4:mpfemale
exports('GetShopPedOutfit', GetShopPedOutfit)                                   -- GetShopPedOutfit(outfitHash)
exports('GetShopPedOutfitComponentVariant', GetShopPedOutfitComponentVariant)   -- GetShopPedOutfitComponentVariant(outfitHash, slot)
exports('GetShopPedOutfitPropVariant', GetShopPedOutfitPropVariant)             -- GetShopPedOutfitPropVariant(outfitHash, slot)
exports('GetShopPedQueryComponent', GetShopPedQueryComponent)                   -- GetShopPedQueryComponent(componentId, componentType, characterType) -- characterType => 0: michael, 1: franklin, 2: trevor, 3:mpmale, 4:mpfemale
exports('GetShopPedQueryComponents', GetShopPedQueryComponents)                 -- GetShopPedQueryComponents(componentType, characterType, locate) -- characterType => 0: michael, 1: franklin, 2: trevor, 3:mpmale, 4:mpfemale
exports('QueryGetComponentIndex', QueryGetComponentIndex)                       -- QueryGetComponentIndex(nameHash, characterType, componentType)
exports('GetShopPedQueryProp', GetShopPedQueryProp)                             -- GetShopPedQueryProp(propId, characterType) -- characterType => 0: michael, 1: franklin, 2: trevor, 3:mpmale, 4:mpfemale
exports('GetShopPedQueryProps', GetShopPedQueryProps)                           -- GetShopPedQueryProps(characterType) -- characterType => 0: michael, 1: franklin, 2: trevor, 3:mpmale, 4:mpfemale
exports('QueryGetPropIndex', QueryGetPropIndex)                                 -- QueryGetPropIndex(nameHash, characterType)